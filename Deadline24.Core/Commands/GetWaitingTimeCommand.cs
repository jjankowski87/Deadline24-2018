using Deadline24.Core.Exceptions;
using Deadline24.Core.Responses;

namespace Deadline24.Core.Commands
{
    public class GetWaitingTimeCommand : Command<Timeout>
    {
        private const string WaitingResponse = "WAITING";

        public GetWaitingTimeCommand(Client client) : base(client)
        {
        }

        public override Timeout SendCommand()
        {
            var respone = Client.ReadLine().Split(' ');
            VerifyResponseLength(respone, 2);

            if (respone[0] != WaitingResponse)
            {
                throw new InvalidResponseException(string.Join(" ", respone), WaitingResponse);
            }

            return new Timeout { Value = ParseDouble(respone, 1) };
        }
    }
}
