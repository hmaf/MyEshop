using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyEshop.Models;

namespace MyEshop.Models
{
    public class AddEditProductViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]

        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int QuantityInStock { get; set; }
        [Required]
        public IFormFile piture { get; set; }
      //  public List<Category> Categories { get; set; }
    }
}
