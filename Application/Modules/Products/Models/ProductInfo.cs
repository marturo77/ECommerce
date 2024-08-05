namespace Application.Modules.Products.Models
{
    /// <summary>
    ///
    /// </summary>
    public class ProductInfo
    {
        /// <summary>
        ///
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ProductInfo()
        {
            OrderItems = new List<OrderItemInfo>();
        }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<OrderItemInfo> OrderItems { get; set; }
    }
}