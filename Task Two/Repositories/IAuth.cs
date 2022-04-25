namespace TaskTwo.Repositories
{
    public interface IAuth<TEntity> where TEntity : class
    {
        public void Save(TEntity entity);
        
        public User GetUser (UserView entity);

        public UserView GetUserView (User user);

        public bool UserExists (string username);

        public bool PasswordMatch (UserView entity);

        public bool PasswordMatch(User user, string pass);

        public string GetToken(UserView entity);
    }
}
