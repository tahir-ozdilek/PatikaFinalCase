using Azure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PatikaFinalProject.DataAccess
{
    public class ShoppingList
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

    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }

        public int ShoppingListID { get; set; }
        [ForeignKey("ShoppingListID")]
        public ShoppingList ShoppingList { get; set; }

        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
    }
}
