namespace TaskTwo.Repositories
{
    public class AuthRepository : IAuth<User>, IAuth<UserView>
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;

        public AuthRepository(APIContext context, IMapper mapper)
        {
            _context=context;
            _mapper=mapper;
        }

        /// <summary>
        /// Получить объект User для контроллера
        /// </summary>
        public UserView GetUserView(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            return _mapper.Map<UserView>(entity);
        }

        /// <summary>
        /// Получить объект User для БД
        /// </summary>
        public User GetUser(UserView entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            return _mapper.Map<User>(entity);
        }

        /// <summary>
        /// Генерация хэша и передача пользователя для записи в БД
        /// пароля
        /// </summary>
        public void Save(UserView entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            User newUser = _mapper.Map<User>(entity);

            newUser=AuthHelper.GetPassword(ref newUser, entity.Password);

            Save(newUser);
        }

        /// <summary>
        /// Запись пользователя в БД
        /// </summary>
        public void Save(User entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _context.Add(entity);

            _context.SaveChanges();
        }

        /// <summary>
        /// Проверка есть ли пользователь в БД
        /// </summary>
        public bool UserExists(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException();
            }

            var findUser = from user in _context.Users
                           where user.Username == username
                           select user;
            
            return findUser.Any();
        }

        /// <summary>
        /// Передача пользователя и пароля для проверки с БД
        /// </summary>
        public bool PasswordMatch(UserView entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            var newUser=_mapper.Map<User>(entity);
            
            return PasswordMatch(newUser,entity.Password);
        }

        /// <summary>
        /// Проверка пароля с сохраненным в БД
        /// </summary>
        public bool PasswordMatch(User entity, string pass)
        {
            var findRecord = from recs in _context.Users
                             where recs.Username == entity.Username
                             select recs;
            
            if (findRecord.Any())
            {
                byte[] passSalt = findRecord.FirstOrDefault().PasswordSalt;
                byte[] passHash = findRecord.FirstOrDefault().PasswordHash;

                return AuthHelper.VerifyPasswordHash(pass, passHash, passSalt);
            }

            return false;
        }

        /// <summary>
        /// Запрос на генерацию токена и возврат токена
        /// </summary>
        public string GetToken(UserView entity)
        {
            var findRecord = from recs in _context.Users
                             where recs.Username == entity.UserName
                             select recs;

            AuthHelper ah = new();
            
            return ah.CreateToken(findRecord.FirstOrDefault());
        }
    }
}
