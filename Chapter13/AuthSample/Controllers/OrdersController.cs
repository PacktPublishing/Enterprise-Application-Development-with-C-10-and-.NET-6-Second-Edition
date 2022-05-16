using AuthSample.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSample.Controllers
{
    [Authorize(Policy = "Over14")]
    public class OrdersController : Controller
    {
        [AuthorizeAgeOver(22)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
