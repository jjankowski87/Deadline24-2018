namespace Deadline24.Core
{
    public interface ICommand<out TResponse> where TResponse : Response
    {
        TResponse SendCommand();
    }
}