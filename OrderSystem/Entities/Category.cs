using System.ComponentModel.DataAnnotations;

namespace OrderSystem.Entities
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }

        public Category() {
            this.CategoryName = CategoryName;
          


        }

    }
}
