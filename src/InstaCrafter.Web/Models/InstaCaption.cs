using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaCrafter.Web.Models
{
    public class InstaCaption
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalCaptionId { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstaUser User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }
}