namespace InstaCrafter.EventBus.Messages
{
    public class UserRequestMessage : IntegrationMessage
    {
        public UserRequestMessage(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }
    }
}