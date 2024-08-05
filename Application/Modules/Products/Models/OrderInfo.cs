namespace Application.Modules.Products.Models
{
    /// <summary>
    ///
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        ///
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        ///
        /// </summary>
        public ICollection<OrderItemInfo> OrderItems { get; set; }
    }
}