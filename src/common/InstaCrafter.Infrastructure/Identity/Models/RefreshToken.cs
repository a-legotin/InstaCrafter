using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InstaCrafter.Infrastructure.Identity.Models
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string? Token { get; set; }
        public DateTime Expiry { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expiry;
        public DateTime Created { get; set; }
        public string? CreatedByIp { get; set; }
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedByToken { get; set; }

        public bool IsActive => Revoked == null && !IsExpired;
    }
}