using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public partial class Message
    {
        [Key]
        public int Id { get; set; }
        public string? SenderId { get; set; }
        public string? ReceiverId { get; set; }
        public DateTime MessageTime { get; set; }
        public string Text { get; set; } = null!;
    }
}
