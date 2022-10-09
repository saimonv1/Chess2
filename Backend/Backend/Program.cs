#region

using Backend.GameHubs;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services
    .AddSignalR()
    .AddNewtonsoftJsonProtocol();

var app = builder.Build();

app.MapHub<GameHub>("/game");

app.UseCors();

app.Run();