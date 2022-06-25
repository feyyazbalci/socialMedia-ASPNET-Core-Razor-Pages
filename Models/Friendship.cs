using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public partial class Friendship
    {
        [Key]
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? ResevierId { get; set; }
        public bool? Approval { get; set; }
    }
}
