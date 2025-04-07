using Microsoft.AspNetCore.Mvc;

namespace EventEasePOE.Controllers
{
    public class BookingsC : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
