using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth<UserView> _context;

        public AuthController(IAuth<UserView> context)
        {
            _context=context;
        }

        /// <summary>
        /// Метод регистрации пользователя в контроллере
        /// </summary>
        [HttpPost("register")]
        public ActionResult Register (UserView request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return BadRequest("Отсутствует имя пользователя или пароль");
            }

            if (_context.UserExists(request.UserName))
            {
                return BadRequest("Пользователь уже зарегистрирован");
            }

            _context.Save(request);

            return Ok("Спасибо за регистрацию");
        }

        /// <summary>
        /// Метод авторизации пользователя в контроллере
        /// </summary>
        [HttpPost("login")]
        public ActionResult<string> Login (UserView request)
        {
            if (request.UserName == null || request.Password == null)
            {
                return BadRequest("Отсутствует имя пользователя или пароль");
            }

            if (!_context.UserExists(request.UserName))
            {
                return BadRequest("Пользователь не найден");
            }

            if (!_context.PasswordMatch(request))
            {
                return BadRequest("Неверный пароль");
            }

            return Ok(_context.GetToken(request));
        }
    }
}
