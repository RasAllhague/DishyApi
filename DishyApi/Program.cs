using DishyApi.Configuration;
using DishyApi.Extensions;
using DishyApi.Models.User;
using DishyApi.Services;
using DishyApi.Services.Dish;
using DishyApi.Services.Foodplan;
using DishyApi.Services.Ingredient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DishyApi;

/// <summary>
/// The main entry point class.
/// </summary>
public static class Program
{
    /// <summary>
    /// The main entry point.
    /// </summary>
    /// <param name="args">The commandline args.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.AddSingletons();
        builder.Services.AddTransient<IPasswordHasher<UserModel>, PasswordHasher<UserModel>>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });
        builder.Services.AddAuthorization();
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
        builder.Services.Configure<MysqlSettings>(builder.Configuration.GetSection("MySql"));
        builder.Services.Configure<ApiKeySettings>(builder.Configuration.GetSection("ApiKey"));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseApiKeyMiddleware();


        app.MapControllers();

        app.Run();
    }

    private static WebApplicationBuilder AddSingletons(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IDbConnService, DbConnService>();
        builder.Services.AddSingleton<IUserService, UserService>();
        builder.Services.AddSingleton<IIngredientService, IngredientService>();
        builder.Services.AddSingleton<IIngredientCategoryService, IngredientCategoryService>();
        builder.Services.AddSingleton<ICategoryService, CategoryService>();
        builder.Services.AddSingleton<IFoodplanService, FoodplanService>();
        builder.Services.AddSingleton<IDishService, DishService>();
        builder.Services.AddSingleton<IImageService, ImageService>();

        return builder;
    }
}