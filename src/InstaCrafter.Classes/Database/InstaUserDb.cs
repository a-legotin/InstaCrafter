using System;
using System.Collections;
using System.Collections.Generic;

namespace InstaCrafter.Classes.Database
{
    public class InstaUserDb
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string ProfilePicture { get; set; }

        public string FullName { get; set; }

        public long InstaIdentifier { get; set; }

        public string ExternalUrl { get; set; }
        public string IsVerified { get; set; }

        public int FollowedByCount { get; set; }
        public DateTime UpdatedTimestamp { get; set; }
        public virtual ICollection<InstaPostDb> PostCollection { get; set; }
        public static InstaUserDb Empty => new InstaUserDb();
    }
}