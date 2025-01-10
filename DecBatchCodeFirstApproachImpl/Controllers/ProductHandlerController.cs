using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DecBatchCodeFirstApproachImpl.Controllers
{
    public class ProductHandlerController : Controller
    {
        private readonly ApplicationDbContext db;
        public IWebHostEnvironment env;
        public ProductHandlerController(ApplicationDbContext db,IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult ProductList()
        {
            var data = db.products.ToList();
            return View(data);
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductView model)
        {
            string path=env.WebRootPath;
            string filepath= "/Content/Images/"+model.Pimg.FileName;
            string fullpath = path + filepath;
            UploadFile(model.Pimg, fullpath);
            var p=new Product()
            {
                PName = model.PName,
                Pcat = model.Pcat,
                Pimg = filepath,
                Price = model.Price
            };
            db.Add(p);
            db.SaveChanges();
            return RedirectToAction("ProductList");
        }
        public void UploadFile(IFormFile file, string fullpath)
        {
            FileStream fs = new FileStream(fullpath, FileMode.Create);
            file.CopyTo(fs);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
