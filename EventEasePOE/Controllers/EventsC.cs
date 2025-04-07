using Microsoft.AspNetCore.Mvc;

namespace EventEasePOE.Controllers
{
    public class EventsC : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
