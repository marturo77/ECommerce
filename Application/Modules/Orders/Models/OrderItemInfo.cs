using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Modules.Products.Models;

namespace Application.Modules.Orders.Models
{
    public class OrderItemInfo
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public OrderInfo Order { get; set; }
        public ProductInfo Product { get; set; }
    }
}