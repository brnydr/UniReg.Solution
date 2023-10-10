using Microsoft.AspNetCore.Mvc;

namespace UniReg.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}