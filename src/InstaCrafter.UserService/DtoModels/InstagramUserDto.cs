namespace InstaCrafter.UserService.DtoModels
{
    public class InstagramUserDto
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
        public string UserName { get; set; }
        public string FullName { get; set; }
        public long Id { get; set; }
    }
}