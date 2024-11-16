using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using DIServiceLifetime.Models;
using DIServiceLifetime.Services;

namespace DIServiceLifetime.Controllers;

public class HomeController : Controller
{

    private readonly IScopedGuidService _scoped1;
    private readonly IScopedGuidService _scoped2;
    
    private readonly ISingletonGuidService _singleton1;
    private readonly ISingletonGuidService _singleton2;
    
    private readonly ITransientGuidService _transient1;
    private readonly ITransientGuidService _transient2;

    public HomeController(
        IScopedGuidService scopedGuidService1, 
        IScopedGuidService scopedGuidService2, 
        ITransientGuidService transientGuidService1, 
        ITransientGuidService transientGuidService2, 
        ISingletonGuidService singletonGuidService1, 
        ISingletonGuidService singletonGuidService2
        ) // Service request လုပ်လိုက်တာ implementation ကို request လုပ်တာ
    {
        _scoped1 = scopedGuidService1;
        _scoped2 = scopedGuidService2;
        _singleton1 = singletonGuidService1;
        _singleton2 = singletonGuidService2;
        _transient1 = transientGuidService1;
        _transient2 = transientGuidService2;
    }

    public IActionResult Index()
    {
        StringBuilder messages = new StringBuilder();
        messages.Append($"Transient 1 : {_transient1.GetGuid()}\n");
        messages.Append($"Transient 2 : {_transient2.GetGuid()}\n");
        
        messages.Append($"Scoped 1 : {_scoped1.GetGuid()}\n");
        messages.Append($"Scoped 2 : {_scoped2.GetGuid()}\n");
        
        messages.Append($"Singleton 1 : {_singleton1.GetGuid()}\n");
        messages.Append($"Singleton 2 : {_singleton2.GetGuid()}\n");

        return Ok(messages.ToString());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}