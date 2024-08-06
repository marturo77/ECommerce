namespace Application.Modules.Orders.Models
{
    /// <summary>
    ///
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// Identificador de la orden
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Fecha de la orden
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Estado de la orden
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Total a pagar por la orden (suma del precio de sus items)
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Listado de items incluidos en la orden
        /// </summary>
        public ICollection<OrderItemInfo> OrderItems { get; set; }
    }
}