using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using Deadline24.Core.Exceptions;

namespace Deadline24.Core
{
    public class Client : IDisposable
    {
        protected const string OkResponse = "OK";
        protected const string FailedResponse = "FAILED";

        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _stream;
        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        public Client(string server, int port, string login, string password)
        {
            _tcpClient = new TcpClient(server, port) { NoDelay = true };
            _stream = _tcpClient.GetStream();
            _writer = new StreamWriter(_stream);
            _reader = new StreamReader(_stream);

            VerifyRead("LOGIN");
            Write(login);

            VerifyRead("PASS");
            Write(password);

            VerifyRead(OkResponse);
        }

        public void Dispose()
        {
            _tcpClient?.Close();
            _stream?.Close();
            _reader?.Close();
            _writer?.Close();
        }

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        public void SendCommand(string command)
        {
            Write(command);

            var response = _reader.ReadLine();
            if (response == OkResponse)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(response))
            {
                throw new InvalidResponseException("Empty Response", OkResponse);
            }

            var responseParts = response.Split(' ');
            if (responseParts[0] != FailedResponse)
            {
                // clean stream
                _reader.ReadToEnd();

                throw new InvalidResponseException(response, FailedResponse);
            }

            var code = 0;
            if (!int.TryParse(responseParts[1], out code))
            {
                throw new InvalidResponseException(response, "Expected error code.");
            }

            var message = string.Join(" ", responseParts.Skip(2));
            throw new CommandException(code, message);
        }

        protected void Write(string command)
        {
            _writer.WriteLine(command);
            _writer.Flush();
        }

        protected void VerifyRead(string expected)
        {
            var response = _reader.ReadLine();
            if (response != expected)
            {
                throw new InvalidResponseException(response, expected);
            }
        }
    }
}