namespace EasySoft.Simple.Single.Application.Areas.ComponentTest.Controllers;

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