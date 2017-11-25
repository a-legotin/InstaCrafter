using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaCrafter.Web.Models
{
    public class InstaCarouselItem
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalCarouselItemId { get; set; }

        public string InstaIdentifier { get; set; }

        public List<InstaMediaInfo> Medias { get; set; } = new List<InstaMediaInfo>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }
    }
}