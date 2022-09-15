using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Areas.ComponentTest.Controllers;

/// <summary>
/// QueryParamController
/// </summary>
public class QueryParamController : AreaControllerCore
{
    /// <summary>
    /// Index
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IActionResult Index(string name)
    {
        ViewData["Name"] = name;

        return View(new Hello
        {
            Name = name
        });
    }

    /// <summary>
    /// Welcome
    /// </summary>
    /// <param name="name"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public string Welcome(string name, int id = 1)
    {
        ViewData["Name"] = name;

        return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is {id}");
    }

    /// <summary>
    /// Privacy
    /// </summary>
    /// <returns></returns>
    public IActionResult Privacy()
    {
        return View();
    }
}