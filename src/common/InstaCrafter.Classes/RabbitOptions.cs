namespace InstaCrafter.Classes
{
    public class RabbitOptions
    {
        public string? Url { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        
        public string? ExchangeName { get; set; }
        
        public string? QueueName { get; set; }
        public int Port { get; set; }
    }
}