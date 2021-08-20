using InstaSharper.Abstractions.Models.User;

namespace InstaCrafter.Media
{
    public class InstaSharperConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string FileStoragePath { get; set; }
    }
    
    internal class UserCredentials : IUserCredentials
    {
        public UserCredentials(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}