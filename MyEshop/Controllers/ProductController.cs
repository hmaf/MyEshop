using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyEshop.Data;

namespace MyEshop.Controllers
{
    public class ProductController : Controller
    {
        private MyEshopContext _context;

        public ProductController(MyEshopContext context)
        {
            _context = context;
        }
        [Route("/group/{Id?}/{name}")]
        public IActionResult Index(int Id,string name)
        {
            ViewData["title"] = name;
            var producs = _context.CategoryToProducts
                .Where(a => a.CategoryId == Id)
                .Include(a => a.Product)
                .Select(a => a.Product)
                .ToList();
            return View(producs);
        }
    }
}
