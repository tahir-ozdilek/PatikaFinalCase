using Azure;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PatikaFinalProject.DataAccess
{
    public class ShoppingList
    {
        public int ID { get; set; }


        public Category? Category { get; set; }

        public ICollection<Product>? ProductList { get; set; }
        
        public bool isBought { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        
        [ForeignKey("ShoppingList")]
        public int ShoppingListID { get; set; }
        //[ForeignKey("ShoppingListID")]
        public ShoppingList ShoppingList { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }

        [ForeignKey("ShoppingList")]
        public int ShoppingListID { get; set; }
        //[ForeignKey("ShoppingListID")]
        public ShoppingList ShoppingList { get; set; }

        public string Name { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
    }
}
