using System.Text.Json.Serialization;
namespace TaskTwo.ControllerModels
{
    /// <summary>
    /// Класс отзыв(рецензия) для контроллера
    /// </summary>
    public class ReviewView
    {
        public Guid ReviewID { get; set; }

        public string? Text { get; set; }

        public int Rating { get; set; }

        public Guid GameID { get; set; }

        [JsonIgnore]
        public GameView? Game { get; set; }
    }
}
