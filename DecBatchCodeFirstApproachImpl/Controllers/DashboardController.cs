using DecBatchCodeFirstApproachImpl.Data;
using Microsoft.AspNetCore.Mvc;

namespace DecBatchCodeFirstApproachImpl.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext db;
        public DashboardController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult UserDashboard()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "User")
            {
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                return View();
            }

        }
        public IActionResult AdminDashboard()
        {
            string role = HttpContext.Session.GetString("Role");
            if (role != "Admin")
            {
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                return View();
            }
        }

        public IActionResult UserList()
        {
            var data = db.users.Where(x => x.Role.Equals("User")).ToList();
            return View(data);
        }

        public IActionResult UpdateStatus(int id)
        {
            var us = db.users.Find(id);
            if (us.Status.Equals("Active"))
            {
                us.Status = "Deactive";
                db.users.Update(us);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
            else
            {
                us.Status = "Active";
                db.users.Update(us);
                db.SaveChanges();
                return RedirectToAction("UserList");
            }
        }
    }
}
