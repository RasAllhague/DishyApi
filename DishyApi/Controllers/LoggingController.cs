using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers;

/// <summary>
/// Api controller base for all controller which use <see cref="ILogger"/>.
/// </summary>
public abstract class LoggingControllerBase : ControllerBase
{
    /// <summary>
    /// The logger used.
    /// </summary>
    protected readonly ILogger _logger;

    /// <summary>
    /// Creates a new instance of <see cref="LoggingControllerBase"/> and injects a <see cref="ILogger"/>
    /// </summary>
    /// <param name="logger">The logger to inject.</param>
    protected LoggingControllerBase(ILogger logger)
    {
        _logger = logger;
    }
}
