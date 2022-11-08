using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers;

public abstract class LoggingControllerBase : ControllerBase
{
    protected readonly ILogger _logger;

    protected LoggingControllerBase(ILogger logger)
    {
        _logger = logger;
    }
}
