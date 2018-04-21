using System;
using System.Globalization;
using Deadline24.Core.Exceptions;

namespace Deadline24.Core
{
    public abstract class Command<TResponse> : ICommand<TResponse> where TResponse: Response
    {
        private static readonly IFormatProvider UkFormatProvider = new CultureInfo("en-gb");

        protected readonly Client Client;

        protected Command(Client client)
        {
            Client = client;
        }

        public abstract TResponse SendCommand();

        protected int? ParseNullableInt(string[] response, int index)
        {
            if (response[index] == "NONE")
            {
                return null;
            }

            return ParseInt(response, index);
        }

        protected int ParseInt(string[] response, int index)
        {
            int result;
            if (int.TryParse(response[index], out result))
            {
                return result;
            }

            throw new InvalidResponseException(response[index], $"Item at index {index} is not integer.");
        }

        protected bool ParseBool(string[] response, int index)
        {
            if (response[index] == "N")
            {
                return false;
            }

            if (response[index] == "Y")
            {
                return true;
            }

            throw new InvalidResponseException(response[index], $"Item at index {index} is not boolean (Y/N).");
        }

        protected double ParseDouble(string[] response, int index)
        {
            float result;
            if (float.TryParse(response[index], NumberStyles.Any, UkFormatProvider, out result))
            {
                return result;
            }

            throw new InvalidResponseException(response[index], $"Item at index {index} is not float.");
        }

        protected void VerifyResponseLength(string[] response, int length)
        {
            if (response.Length != length)
            {
                throw new InvalidResponseException(string.Join(" ", response), $"Response must have {length} items, but have {response.Length}.");
            }
        }
    }
}