using System.Text.Json.Serialization;

namespace TaskTwo.Models
{
    public class ReviewDB
    {
        [Key]
        [Required]
        public Guid ReviewID { get; set; }

        [Required]
        public string? Text { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public Guid GameID { get; set; }
        
        [JsonIgnore]
        public GameDB? Game { get; set; }
    }
}
