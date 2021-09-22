using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyEshop.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public bool IsFainaly { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
