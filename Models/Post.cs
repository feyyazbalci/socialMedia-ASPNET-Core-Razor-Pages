using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int userId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public DateTime postTime { get; set; }


    }
}
