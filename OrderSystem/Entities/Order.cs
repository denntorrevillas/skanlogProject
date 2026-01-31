using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Entities
{
    public class Order
    {
        [Key]
        public int orderId { get; set; }

     
        public int CustomerID { get; set; }

   
        
        public DateTime orderDate  = DateTime.Now;



        public int totaAmount { get; set; }

        public List<OrderItem> items { get; set; } = new List<OrderItem>();
    }
}
