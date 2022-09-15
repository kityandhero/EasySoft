using Microsoft.AspNetCore.Mvc;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// RemoteErrorLogController
/// </summary>
public class RemoteErrorLogController : AreaControllerCore
{
    /// <summary>
    /// Test
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public IActionResult Test()
    {
        throw new Exception("Test Exception");
    }
}