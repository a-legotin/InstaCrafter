using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaCrafter.Web.Models
{
    public class InstaMediaInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalMediaId { get; set; }

        public string Url { get; set; }
        
        public int Width { get; set; }

        public int Height { get; set; }
    }
}