using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Controllers
{
    [Route("api")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGame<GameView> _context;
        private readonly IMapper _mapper;

        public GameController(IGame<GameView> context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Метод "Получить все игры" в контроллере
        /// </summary>
        [HttpGet("/all"), AllowAnonymous]
        public ActionResult <IEnumerable<GameView>> GetAll()
        {
           var gameType= new GameView();

           return Ok( _context.GetAll(gameType));
        }
        
        /// <summary>
        /// Метод "Получить все игры по убыванию" в контроллере
        /// </summary>
        [HttpGet("/all/descending")]
        public ActionResult <IEnumerable<GameView>> GetAllDescending()
        {
            Dictionary<GameView, double> gamesAndRatings = _context.GetAllDescending();
            
            var result = gamesAndRatings.Select(x => 
                new { 
                        x.Key.GameID, 
                        x.Key.Name, 
                        x.Key.Genre,
                        x.Value 
                    });
            
            return Ok(result);
        }

        /// <summary>
        /// Метод "Получить игру и все отзывы на нее" в контроллере
        /// </summary>
        [HttpGet("/{id}/reviews")]
        public ActionResult<GameView> GetReviews(Guid id)
        {
            if (!_context.GameExists(id))
            {
                return BadRequest("Игры с указанным id нет в БД");
            }

            return Ok(_context.GetReviews(id));
        }

        /// <summary>
        /// Метод "Добавить игру" в контроллере
        /// </summary>
        [HttpPost("/add")]
        public ActionResult Add(GameAdd g)
        {
            if (g.Genre==null || g.Name==null)
            {
                return BadRequest("Некорректный запрос");
            }

            _context.Add(_mapper.Map<GameView>(g));

            return Ok();
        }

        /// <summary>
        /// Метод "Удалить игру" в контроллере
        /// </summary>
        [HttpDelete("/delete")]
        public ActionResult Delete(Guid id)
        {
            if (!_context.GameExists(id))
            {
                return BadRequest("Игры с указанным id нет в БД");
            }

            GameView g = new()
            {
                GameID = id
            };

            _context.Delete(g);

            return Ok();
        }
    }
}
