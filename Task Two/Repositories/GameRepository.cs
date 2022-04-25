
namespace TaskTwo.Repositories
{
    public class GameRepository : IGame<GameDB>, IGame<GameView>
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;

        public GameRepository(APIContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Передаем добавление пользователя на запись
        /// </summary>
        public void Add(GameView gamev)
        {
            if (gamev == null)
            {
                throw new ArgumentNullException();
            }

            var game = _mapper.Map<GameDB>(gamev);

            Add(game);
        }

        /// <summary>
        /// Передаем удаление пользователя на запись
        /// </summary>
        public void Delete(GameView gamev)
        {
            if (gamev == null)
            {
                throw new ArgumentNullException();
            }

            var game = _mapper.Map<GameDB>(gamev);

            Delete(game);
        }
       
        /// <summary>
        /// Передаем все фильмы в контроллер
        /// </summary>
        public IEnumerable<GameView> GetAll(GameView g)
        {
            var typeIndicator = new GameDB();

            List<GameDB> tList = GetAll(typeIndicator).ToList();
            
            List<GameView> returnList = _mapper.Map<List<GameView>>(tList);
            
            return returnList;
        }

        /// <summary>
        /// Передаем фильмы с рейтингами в контроллер
        /// </summary>
        public Dictionary<GameView, double> GetAllDescending()
        {
            Dictionary<GameView, double> result = new ();

            Dictionary<GameDB, double> gamesDB = GetDescending();

            foreach(var t in gamesDB)
            {
                GameView gameView = _mapper.Map<GameView>(t.Key);

                result.Add(gameView, t.Value);
            }

            return result;
        }

        /// <summary>
        /// Передаем в контроллер игру и рецензии
        /// </summary>
        public GameView GetReviews(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            GameView nuView =_mapper.Map<GameView> (GetReview(id));

            return nuView;
        }

        /// <summary>
        /// Запись игры в БД
        /// </summary>
        public async void Add (GameDB game)
        {
            if (game == null)
            {
                throw new ArgumentNullException();
            }
            
            _context.Games.Add(game);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление игры в БД
        /// </summary>
        public async void Delete(GameDB game)
        {
            if (game == null)
            {
                throw new ArgumentNullException();
            }
            
            _context.Games.Remove(game);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получаем из БД все игры
        /// </summary>
        public IEnumerable<GameDB> GetAll(GameDB gdb)
        {
            return _context.Games.ToList();
        }

        /// <summary>
        /// Получаем из БД все игры и считаем их рейтинг
        /// </summary>
        public Dictionary<GameDB, double> GetDescending()
        {
            Dictionary<GameDB, double> resultList = new();

            List<GameDB> temp = _context.Games.ToList ();

            List<ReviewDB> temprevs = _context.Reviews.ToList ();

            foreach(GameDB gameDB in temp)
            {
                var getRevs = _context.Reviews
                                .Where(x => x.GameID == gameDB.GameID)
                                .Select(r => r.Rating);

                if (getRevs.Any())
                {
                    resultList.Add(gameDB,Math.Round((double)getRevs.Average(),2));
                }
                else
                {
                    resultList.Add(gameDB, 0);
                }
            }
            
            var result = from p in resultList
                         orderby p.Value descending
                         select p;

            return result.ToDictionary(x=>x.Key,y=>y.Value);
        }
        
        /// <summary>
        /// Получаем из БД игру и включаем туда все отзывы
        /// </summary>
        public GameDB GetReview(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }

            var resList = _context.Games
                        .Where(i => i.GameID == id)
                        .Include(revs => revs.ReviewList);

            if (resList.Any())
            {
                GameDB? res = resList.FirstOrDefault();

                return res;
            }
            else
                return null;
        }
        
        /// <summary>
        /// Проверяем есть ли игра в БД
        /// </summary>
        public bool GameExists (Guid id)
        {
            var gameFound=from games in _context.Games
                          where games.GameID == id
                          select games;

            return gameFound.Any();
        }
    }
}
