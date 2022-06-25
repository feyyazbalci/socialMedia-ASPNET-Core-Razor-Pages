using System.ComponentModel.DataAnnotations;



namespace termProject_201811010.Models
{
    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "Profile Picture")]
        public IFormFile? FormFile { get; set; }
    }
}
