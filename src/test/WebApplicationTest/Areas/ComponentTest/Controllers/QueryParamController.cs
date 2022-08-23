using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

public class QueryParamController : AreaControllerCore
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