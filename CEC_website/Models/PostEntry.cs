using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CEC_website.Models
{
    public class PostEntry
    {
        
        public int Id { get; set; }

        [Required]
        public required string Title { get; set; } = string.Empty;

        [Required]
        public required string Content { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public DateTime DayPosted { get; set; } = DateTime.Now;    
    }
}
