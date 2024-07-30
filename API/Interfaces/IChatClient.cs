namespace API.Interfaces
{
    public interface IChatClient
    {
        public Task ReceivedMessageAsync(string userName, string message);
    }
}