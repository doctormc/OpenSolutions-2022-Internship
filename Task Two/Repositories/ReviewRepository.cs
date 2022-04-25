namespace TaskTwo.Repositories
{
    public class ReviewRepository : IReview<ReviewDB>,IReview<ReviewView>
    {
        private readonly APIContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(APIContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Получаем из контроллера отзыв и передаем на запись
        /// </summary>
        public void Add(ReviewView review)
        {
            if(review == null)
            {
                throw new ArgumentNullException();
            }
            
            Add(_mapper.Map<ReviewDB>(review));
        }

        /// <summary>
        /// Запись отзыва в БД
        /// </summary>
        public async void Add(ReviewDB review)
        {
            _context.Reviews.Add(review);

            await _context.SaveChangesAsync();
        }
    }
}
