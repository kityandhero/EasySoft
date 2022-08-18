using System.Text.Encodings.Web;
using EasySoft.Core.Web.Framework.Controllers;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers;

public class MvcTestController : CustomControllerBase
{
    public IActionResult Index(string name)
    {
        ViewData["Name"] = name;

        return View(new Hello
        {
            Name = name
        });
    }

    public string Welcome(string name, int id = 1)
    {
        ViewData["Name"] = name;

        return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is {id}");
    }

    public IActionResult Privacy()
    {
        return View();
    }
}