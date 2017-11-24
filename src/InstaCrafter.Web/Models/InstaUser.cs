using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaBackup.Models
{
    public class InstaUser
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalUserId { get; set; }

        public virtual ICollection<InstaStory> Stories { get; set; } = new List<InstaStory>();
        public virtual ICollection<InstaMediaPost> Medias { get; set; } = new List<InstaMediaPost>();

        public bool IsVerified { get; set; }
        public bool IsPrivate { get; set; }
        public long Pk { get; set; }
        public string ProfilePicture { get; set; }
        public string ProfilePictureId { get; set; } = "unknown";
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}