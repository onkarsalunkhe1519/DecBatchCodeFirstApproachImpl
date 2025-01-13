using DecBatchCodeFirstApproachImpl.Data;
using DecBatchCodeFirstApproachImpl.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IActionResult EditProduct(int id)
        {
            var data = db.products.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            return View(data); // Pass the product data to the view
        }

        [HttpPost]
        public IActionResult EditProduct(Product model, IFormFile Pimage)
        {
            var product = db.products.Find(model.Pid);
            if (product == null)
            {
                return NotFound();
            }

            // Update fields
            product.PName = model.PName;
            product.Pcat = model.Pcat;
            product.Price = model.Price;

            // Handle image upload
            if (Pimage != null)
            {
                string path = env.WebRootPath;
                string filepath = "/Content/Images/" + Pimage.FileName;
                string fullpath = path + filepath;

                UploadFile(Pimage, fullpath); // Save the new file

                // Update product image path
                product.Pimg = filepath;
            }

            db.Update(product);
            db.SaveChanges();

            return RedirectToAction("ProductList");
        }

    }
}
