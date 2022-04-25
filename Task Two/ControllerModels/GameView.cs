namespace TaskTwo.ControllerModels
{
    /// <summary>
    /// Класс игры для контроллера
    /// </summary>
    public class GameView
    {
        public Guid GameID { get; set; }

        public string? Name { get; set; }

        public string? Genre { get; set; }

        public List<ReviewView>? ReviewList { get; set; } = new List<ReviewView>();
    }
    
}
