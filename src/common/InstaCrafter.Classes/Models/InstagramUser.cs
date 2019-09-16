namespace InstaCrafter.Classes.Models
{
    public class InstagramUser
    {
        public bool HasAnonymousProfilePicture { get; set; }
        public int FollowersCount { get; set; }
        public string FollowersCountByLine { get; set; }
        public string SocialContext { get; set; }
        public string SearchSocialContext { get; set; }
        public int MutualFollowers { get; set; }
        public int UnseenCount { get; set; }
        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePictureId { get; set; } = "unknown";
        public string UserName { get; set; }
        public string FullName { get; set; }
        public static InstagramUser Empty => new InstagramUser {Pk = 0};
    }
}