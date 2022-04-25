namespace TaskTwo.Models
{
    public class GameDB
    {
        [Key]
        [Required]
        public Guid GameID { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Genre { get; set; }

        [Required]
        public ICollection<ReviewDB>? ReviewList { get; set; }=new List<ReviewDB>();
    }
} 
