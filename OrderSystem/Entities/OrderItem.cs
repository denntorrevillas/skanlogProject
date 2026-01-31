using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        public int ProductID { get; set; }

       public int quantity { get; set; }
        public int subtotal { get; set; }
    }
}
