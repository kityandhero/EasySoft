using EasySoft.Core.Infrastructure.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.ConsulRegistrationCenterClient.Controllers;

public class ConsulServiceHealthController : BasicController
{
    public IActionResult Index()
    {
        return Ok("ok");
    }
}