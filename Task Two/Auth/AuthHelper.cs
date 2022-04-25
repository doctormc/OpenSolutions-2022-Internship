namespace TaskTwo.Auth
{
    public class AuthHelper
    {
        public const string SECRET_KEY = "Open Solutions 2022";

        /// <summary>
        /// Метод создания токена
        /// </summary>
        public string CreateToken(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("no user");
            }

            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name,user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
                (claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Метод проверки токена
        /// </summary>
        public string ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException("no token");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
            
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                tokenHandler.ValidateToken(token, tokenValidationParams
                    ,out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var username = jwtToken.Claims.First(x=>x.Type=="Name").Value;

                return username;
            }
            catch (SecurityTokenValidationException ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Метод генерации хэша пароля для хранения в БД
        /// </summary>
        public static User GetPassword(ref User user,string pass)
        {
            if (user == null || string.IsNullOrEmpty(pass))
            {
                throw new ArgumentNullException("Нет пароля или юзера");
            }

            using(var crypto = new HMACSHA512())
            {
                user.PasswordSalt = crypto.Key;

                user.PasswordHash= crypto.ComputeHash(Encoding.UTF8.GetBytes(pass));

                return user;
            }
        }
        
        /// <summary>
        /// Метод проверки пароля от пользователя по хэшу
        /// </summary>
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var cryptoalg = new HMACSHA512(passwordSalt))
            {
                var computeHash = cryptoalg.ComputeHash(Encoding.UTF8.GetBytes(password));

                return computeHash.SequenceEqual(passwordHash);
            }
        }
    }
}
