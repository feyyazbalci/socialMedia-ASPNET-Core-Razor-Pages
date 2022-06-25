using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public partial class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }
        public byte[]? Picture { get; set; }

        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? City { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool HideProfile { get; set; } = false;
    }
}
