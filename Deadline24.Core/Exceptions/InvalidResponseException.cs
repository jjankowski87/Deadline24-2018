namespace Deadline24.Core.Exceptions
{
    public class InvalidResponseException : ServerExceptionBase
    {
        public InvalidResponseException(string actual, string expected)
            : base($"Invalid server response, expected: '{expected}' but received '{actual}'.")
        {
        }
    }
}