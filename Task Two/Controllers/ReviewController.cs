using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReview<ReviewView> _context;

        public ReviewController(IReview<ReviewView> context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод добавления рецензии через объект, авторизация
        /// </summary>
        [HttpPost("/review/add"), Authorize]
        public ActionResult Add(ReviewView rev)
        {
            if (rev.ReviewID != Guid.Empty)
            {
                return BadRequest("Некорректный запрос");
            }

            if (rev.GameID == Guid.Empty)
            {
                return BadRequest("Не указана игра для которой рецензия");
            }

            if (rev.Rating < 0 || rev.Rating > 5)
            {
                return BadRequest("Рейтинг должен быть от 0 до 5");
            }

            if (rev.Rating.GetType() != typeof(int))
            {
                return BadRequest("Рейтинг может быть только целым числом");
            }

            if (string.IsNullOrEmpty(rev.Text) || rev.Text is null)
            {
                rev.Text = " ";
            }

            _context.Add(rev);

            return Ok();
        }

        /// <summary>
        /// Метод добавления рецензии простой, авторизация
        /// </summary>
        [HttpPost("/review/add/simple"), Authorize]
        public ActionResult AddSimple(Guid Game_id, string text, int rating)
        {
            if (Game_id == Guid.Empty)
            {
                return BadRequest("Не указана игра для которой рецензия");
            }

            if (rating < 0 || rating > 5)
            {
                return BadRequest("Рейтинг должен быть от 0 до 5");
            }

            if (rating.GetType() != typeof(int))
            {
                return BadRequest("Рейтинг может быть только целым числом");
            }

            ReviewView newRev = new()
            {
                GameID = Game_id,
                Text = text,
                Rating = rating,
            };

            _context.Add(newRev);

            return Ok();
        }
    }
}
