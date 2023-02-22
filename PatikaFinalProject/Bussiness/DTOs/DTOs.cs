using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PatikaFinalProject.DataAccess
{
    public class ShoppingListDTO
    {
        public int ID { get; set; }

        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

        public ICollection<Product> ProductList { get; set; }

        public bool isBought { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly CompletedDate { get; set; }
    }
    public class ShoppingListCreateDTO
    {
        public int CategoryID { get; set; }
        public bool isBought { get; set; }
        public DateOnly CreationDate { get; set; }
        public DateOnly CompletedDate { get; set; }
    }

    public class CategoryDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CategoryCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class ProductDTO
    {
        public int ID { get; set; }

        public int ShoppingListID { get; set; }
        [ForeignKey("ShoppingListID")]
        public ShoppingList ShoppingList { get; set; }

        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
    }
    public class ProductCreateDTO
    {
        public int ShoppingListID { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
    }
}
