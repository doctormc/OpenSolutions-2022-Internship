namespace TaskTwo.Repositories
{
    public interface IReview<TEntity> where TEntity : class
    {
        public void Add(TEntity entity);
    }
}
