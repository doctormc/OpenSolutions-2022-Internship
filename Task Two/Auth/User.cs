namespace TaskTwo.Auth
{
    /// <summary>
    /// Класс пользователя для БД
    /// </summary>
    public class User
    {
        [Key]
        public string? Username { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }
    }
}
