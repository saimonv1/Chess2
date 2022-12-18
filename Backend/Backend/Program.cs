#region

using Backend.GameHubs;
using Microsoft.AspNetCore.SignalR;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder
                //.WithOrigins(("http://localhost:3000"))
                .WithOrigins("https://chess2-front-fiv67.ondigitalocean.app")
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