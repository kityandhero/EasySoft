using EasySoft.Core.Infrastructure.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.ConsulConfigClient.Controllers;

public class ConsulServiceHealthController : BasicController
{
    public IActionResult Index()
    {
        return Ok("ok");
    }
}