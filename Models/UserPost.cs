using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public partial class UserPost
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Message { get; set; }
        public byte[]? Picture { get; set; }
        public DateTime? Timestamp { get; set; }
        public int Dislikes { get; set; }
        public int Likes { get; set; }
    }
}
