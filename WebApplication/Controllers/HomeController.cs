using System.Web.Mvc;
using WebApplication.Managers;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        protected IUserManager UserManager;

        public HomeController(IUserManager userManager)
        {
            UserManager = userManager;
        }
        public ActionResult Index()
        {
            // The userid would be determined elsewhere in real life
            var user = UserManager.GetUser(1);
            return View(user);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}