using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
