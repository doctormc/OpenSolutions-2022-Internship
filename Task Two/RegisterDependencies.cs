namespace TaskTwo
{
    public static class RegisterDependencies  
    {
        public static IServiceCollection AdditionalServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<APIContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IGame<GameDB>, GameRepository>();

            services.AddScoped<IReview<ReviewDB>, ReviewRepository>();

            services.AddScoped<IGame<GameView>, GameRepository>();
            
            services.AddScoped<IReview<ReviewView>, ReviewRepository>();
            
            services.AddScoped<IAuth<User>, AuthRepository>();
            
            services.AddScoped<IAuth<UserView>, AuthRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                               (config.GetSection("AppSettings:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Authorization header (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}
