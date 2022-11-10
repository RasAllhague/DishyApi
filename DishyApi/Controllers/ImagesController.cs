using DishyApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DishyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : LoggingControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IImageService _imageService;

    public ImagesController(ILogger logger, ITokenService tokenService, IImageService imageService) : base(logger)
    {
        _tokenService = tokenService;
        _imageService = imageService;
    }

    // GET api/<IImageController>/5
    [HttpGet("{id}")]
    public string GetImageAsync(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/<IImageController>/Dishes/{id}
    [HttpPost("Dishes/{id}")]
    public void UploadImageForDishAsync(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/<IImageController>/Ingredients/{id}
    [HttpPost("Ingredients/{id}")]
    public void UploadImageForIngredientAsync(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/<IImageController>/Foodplans/{id}
    [HttpPost("Foodplans/{id}")]
    public void UploadImageForFoodplanAsync(int id)
    {
        throw new NotImplementedException();
    }
}
