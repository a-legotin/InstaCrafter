using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrafter.Models
{
    public class InstaPost
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }

        public string Link { get; set; }

        public bool CanViewComment { get; set; }

        public DateTime CreatedTime { get; set; }

    }

}
