using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DishyApi.Controllers;

/// <summary>
/// Api controller for getting the current server version.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class VersionController : ControllerBase
{
    /// <summary>
    /// Retrieves the current server version.
    /// </summary>
    /// <returns>A string containing the server version.</returns>
    [HttpGet]
    public string Get()
    {
        return GetType().Assembly.GetName().Version.ToString();
    }
}
