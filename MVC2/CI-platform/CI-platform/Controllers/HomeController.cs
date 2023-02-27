//using CI_platform.Entities.DataModels;
using CI_platform.Models;
using CI_Platform.Entity.DataModels;
using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;


namespace CI_platform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        //private readonly ilogger<homecontroller> _logger;

        //public homecontroller(ILogger<homecontroller> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(User obj)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == obj.Email);

                if (user == null)
                {
                    TempData["error"] = "Invalid User!!";
                    return View(obj);
                }
                else
                {
                    if (user.Password == obj.Password)
                    {
                        TempData["success"] = "Logged In Successfully";
                        return RedirectToAction("HomePage");
                    }
                }
            }
            return View(obj);
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User userData)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _db.Users.Add(userData);
                    _db.SaveChanges();
                    TempData["success"] = "Employee added successfully";
                    return RedirectToAction("Index");


                }
                else
                {
                    TempData["error"] = "Model Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();

            }

        }
        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult StoryListing()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}