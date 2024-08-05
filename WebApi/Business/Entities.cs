using System.ComponentModel.DataAnnotations;

namespace Business
{
    // Models/Customer.cs
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }

    // Models/Order.cs
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

    // Models/OrderItem.cs
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

    // Models/Product.cs

    public class Product
    {
        /// <summary>
        ///
        /// </summary>
        public Product()
        {
            OrderItems = new List<OrderItem>();
        }

        public int ProductId { get; set; }

        [Required(ErrorMessage = "The name field is required.")]
        [StringLength(100, ErrorMessage = "The name must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description field is required.")]
        [StringLength(500, ErrorMessage = "The description must be between 5 and 500 characters.", MinimumLength = 5)]
        public string Description { get; set; }

        [Required(ErrorMessage = "The price field is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The category field is required.")]
        [StringLength(50, ErrorMessage = "The category must be between 3 and 50 characters.", MinimumLength = 3)]
        public string Category { get; set; }

        [Required(ErrorMessage = "The stock field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The stock must be a non-negative number.")]
        public int Stock { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}