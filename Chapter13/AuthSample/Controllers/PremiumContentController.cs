using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers
{
    [Authorize(Policy = "PremiumContentPolicy")]
    public class PremiumContentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
