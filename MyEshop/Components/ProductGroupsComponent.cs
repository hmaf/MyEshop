using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Components
{
    public class ProductGroupsComponent : ViewComponent
    {
        private MyEshopContext _context;

        public ProductGroupsComponent(MyEshopContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var ShowGroup = _context.Categories
                .Select(a => new ShowGroupViewModel()
                {
                    GroupId = a.Id,
                    Name = a.Name,
                    GroupCount = _context.CategoryToProducts.Count(s => s.CategoryId == a.Id)

                }).ToList();
            return View("/Views/Components/ProductGroupsComponent.cshtml", ShowGroup);
        }
    }
}
