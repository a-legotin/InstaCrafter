namespace InstaCrafter.Core.Constants
{
    public static class ApplicationIdentityConstants
    {
        public static readonly string DefaultPassword = "Passw0rd!";

        public static class Roles
        {
            public static readonly string Administrator = "Administrator";
            public static readonly string Member = "Member";

            public static readonly string[] RolesSupported = {Administrator, Member};
        }
    }
}