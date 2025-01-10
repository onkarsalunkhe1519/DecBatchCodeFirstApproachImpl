using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Filter;
using DecBatchCodeFirstApproachImpl.Models;
using Microsoft.AspNetCore.Mvc;

namespace DecBatchCodeFirstApproachImpl.Controllers
{
    public class EmpController : Controller
    {
        private readonly ApplicationDbContext db;
        public EmpController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if(role!="Admin")
            {
                return RedirectToAction("SignIn", "Auth");
            }
            else
            {
                var data = db.emp.ToList();
                return View(data);
            }
            
        }

        public IActionResult AddEmp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmp(Employee e)
        {
            if(ModelState.IsValid)
            {
                db.emp.Add(e);
                db.SaveChanges();
                TempData["msg"] = "Emp Added Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
       
        public IActionResult DeleteEmp(int id)
        {
            var data=db.emp.Find(id);
            //var data=db.emp.Where(s => s.Id.Equals(id)).SingleOrDefault();
            if(data!=null)
            {
                db.emp.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public IActionResult EditEmp(int id)
        {
            var data=db.emp.Find(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult EditEmp(Employee e)
        {
            db.emp.Update(e);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
