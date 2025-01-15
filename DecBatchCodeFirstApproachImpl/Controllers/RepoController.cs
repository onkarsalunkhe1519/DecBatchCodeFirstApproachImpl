using DecBatchCodeFirstApproachImpl.Models;
using DecBatchCodeFirstApproachImpl.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DecBatchCodeFirstApproachImpl.Controllers
{
    public class RepoController : Controller
    {
        public IEmpService empService;
        public RepoController(IEmpService empService) {
            this.empService = empService;
                }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Fetch()
        {
            var d= empService.FetchEmployes();
            return View(d);
        }
        [HttpPost]
        public IActionResult Fetch(string str, string dept, int no, string sort)
        {
            if (str == null&& dept==null&&no==0&&sort=="def")
            {


                var d = empService.FetchEmployes();
                return View(d);
            }
            else
            {
                var d = empService.SearchEmployees(str,dept,no,sort);
                return View(d);
            }
        }

        public IActionResult Delete(int id)
        {
            empService.DeleteById(id);
            TempData["Error"] = "Employee Deleted";
            return RedirectToAction("Fetch");
        }
        public IActionResult EditEmp(int id)
        {
            var d = empService.FindEmpById(id);
            return View(d);
        }
        [HttpPost]
        public IActionResult EditEmp(Employee em)
        {
            empService.UpdateEmp(em);
            TempData["Error"] = "Employee Deleted";
            return RedirectToAction("Fetch");
        }
    }
}

