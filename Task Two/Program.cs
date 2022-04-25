
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AdditionalServices(builder.Configuration);

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//app.UseMiddleware<TokenMiddleware>();

app.Run();

