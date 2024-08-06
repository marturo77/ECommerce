using Application.Modules.Products.Models;

namespace Application.Modules.Orders.Models
{
    /// <summary>
    /// Representa un item dentro de una orden o pedido
    /// </summary>
    public class OrderItemInfo
    {
        /// <summary>
        /// Identificadcor del item dentro de la orden
        /// </summary>
        public int OrderItemId { get; set; }

        /// <summary>
        /// Identificador de la orden
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Identificador del producto
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Cantidad de producto
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Precio al cual se compro
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Entidad orden
        /// </summary>
        public OrderInfo Order { get; set; }

        /// <summary>
        /// Entidad producto
        /// </summary>
        public ProductInfo Product { get; set; }
    }
}