using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Entities
{
    public class Product
    {
        [Key]
        public int ProductID {  get; set; }

        [MaxLength(50)]
        public string ProductName {get; set; }

        public int price { get; set; }

        public int stock { get; set; }
        public int CategoryID { get; set; }

        public Product()
        {
            this.ProductName = ProductName;
            this.ProductID = ProductID;
            this.CategoryID = CategoryID;
            
        }

    }
}
