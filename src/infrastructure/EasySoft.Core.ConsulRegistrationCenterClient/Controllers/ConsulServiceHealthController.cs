namespace EasySoft.Core.ConsulRegistrationCenterClient.Controllers;

public class ConsulServiceHealthController : BasicController
{
    public IActionResult Index()
    {
        return Ok("ok");
    }
}