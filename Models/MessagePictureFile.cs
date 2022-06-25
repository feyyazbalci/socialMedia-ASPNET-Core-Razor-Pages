using System.ComponentModel.DataAnnotations;

namespace termProject_201811010.Models
{
    public class MessagePictureFile
    {
        [Display(Name = "Picture")]
        public IFormFile? FormFile { get; set; }
    }
}
