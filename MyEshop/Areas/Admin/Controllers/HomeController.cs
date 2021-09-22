using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;
using MyEshop.Models;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace MyEshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private MyEshopContext _context;

        public HomeController(MyEshopContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var product = _context.Products.Include(i => i.Item).ToList();
            return View(product);
        }

       [HttpGet]
        public IActionResult Add()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddEditProductViewModel productView)
        {
            //if (ModelState.IsValid)
            //{
            //    return View();
            //}
            var item=new Item()
            {
                Price = productView.Price,
                QuantityInStock = productView.QuantityInStock
            };
            _context.Items.Add(item);
            _context.SaveChanges();
            var pro=new Product()
            {
                Name = productView.Name,
                Description = productView.Description,
                Item = item
            };
            _context.Products.Add(pro);
            _context.SaveChanges();
            pro.ItemId = pro.Id;
            _context.SaveChanges();

            if (productView.piture?.Length > 0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "images",
                    pro.Id + Path.GetExtension(productView.piture.FileName));
                using (var stream = new FileStream(filePath,FileMode.Create))
                {
                    productView.piture.CopyTo(stream);
                }
            }

            //_context.SaveChanges();
            //if (selectedGroups.Any() && selectedGroups.Count > 0)
            //{
            //    foreach (int gr in selectedGroups)
            //    {
            //        _context.CategoryToProducts.Add(new CategoryToProduct()
            //        {
            //            CategoryId = gr,
            //            ProductId = pro.Id
            //        });
            //    }

            //    _context.SaveChanges();
            //}
            return RedirectToAction("Index");
        }

        
        [HttpGet]
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           var EditProduct = _context.Products.Include(i => i.Item)
                .Where(a => a.Id == id)
                .Select(s => new AddEditProductViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    Price = s.Item.Price,
                    QuantityInStock = s.Item.QuantityInStock
                }).FirstOrDefault();
            
            return View(EditProduct);
        }

        [HttpPost]
        public IActionResult Update(AddEditProductViewModel edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            var product = _context.Products.Find(edit.Id);
            var item = _context.Items.First(i => i.Id == product.ItemId);
            product.Name = edit.Name;
            product.Description = edit.Description;
            item.Price = edit.Price;
            item.QuantityInStock = edit.QuantityInStock;
            _context.SaveChanges();

            if (edit.piture?.Length>0)
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    product.Id + Path.GetExtension(edit.piture.FileName));
                using (var stream=new FileStream(filePath,FileMode.Create))
                {
                    edit.piture.CopyTo(stream);
                };
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(a=>a.Id==id);

            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product p)
        {
            
            var pro = _context.Products.Find(p.Id);
            var item = _context.Items.First(a => a.Id == pro.ItemId);
            _context.Products.Remove(pro);
            _context.Items.Remove(item);
            _context.SaveChanges();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot",
                "Images",
                pro.Id + ".jpg");

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return RedirectToAction("Index");
        }
    }
}
