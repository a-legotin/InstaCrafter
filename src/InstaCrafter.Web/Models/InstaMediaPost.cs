﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaBackup.Models
{
    public class InstaMediaPost
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalPostId { get; set; }

        public DateTime? TakenAt { get; set; }
        public string Pk { get; set; }
        public string InstaIdentifier { get; set; }
        public DateTime? DeviceTimeStap { get; set; }
        public int MediaType { get; set; }
        public string Code { get; set; }
        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }
        public string ImageUrl { get; set; }
        public int Width { get; set; }
        public string Height { get; set; }
        public InstaUser User { get; set; }
        public string TrakingToken { get; set; }
        public int LikesCount { get; set; }
        public string Caption { get; set; }
        public string CommentsCount { get; set; }
        public bool PhotoOfYou { get; set; }
        public bool HasLiked { get; set; }
        public int ViewCount { get; set; }
        public bool HasAudio { get; set; }
        public bool IsMultiPost { get; set; }
    }
}