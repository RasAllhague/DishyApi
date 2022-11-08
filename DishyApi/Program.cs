using DishyApi.Models.User;
using DishyApi.Services;
using Microsoft.AspNetCore.Identity;

namespace DishyApi;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IDbConnService>(s => new DbConnService("Server=localhost;Database=DishDb;Uid=root;Pwd=PENIS;"));
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddTransient<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}