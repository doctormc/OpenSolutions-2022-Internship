namespace TaskTwo.Repositories
{
    public interface IGame<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);

        public void Delete(TEntity entity);

        public IEnumerable<TEntity> GetAll(TEntity entity);

        public Dictionary<GameView, double> GetAllDescending();

        public GameDB GetReview(Guid id);

        public GameView GetReviews(Guid id);

        public bool GameExists (Guid id);
    }
}
