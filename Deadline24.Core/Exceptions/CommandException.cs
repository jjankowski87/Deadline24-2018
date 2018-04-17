namespace Deadline24.Core.Exceptions
{
    public class CommandException : ServerExceptionBase
    {
        public CommandException(int code, string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; private set; }
    }
}