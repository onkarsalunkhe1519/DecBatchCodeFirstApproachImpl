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
        public IActionResult Fetch()
        {
            var d = empService.FetchEmployes();
            return View(d);
        }
    }
}
