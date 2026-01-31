using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }

        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerEmail { get; set; }
        [Required]
        public string CustomerPhone { get; set; }

        public Customer()
        {
            this.CustomerName = CustomerName;
            this.CustomerPhone = CustomerPhone;
            this.CustomerEmail = CustomerEmail;
            
        }

    }
}
