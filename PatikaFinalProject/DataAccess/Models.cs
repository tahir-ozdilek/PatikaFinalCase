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


    public class Movie
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public int MovieTypeID { get; set; }
        [ForeignKey("MovieTypeID")]
        public MovieType? MovieType { get; set; }
        public DateTime MovieYear { get; set; }
        public int DirectorID { get; set; }
        [ForeignKey("DirectorID")]
        public Director? Director { get; set; }
        public int Price { get; set; }
        public bool IsSold { get; set; } = false;
    }




    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class Order
    {
        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie? Movie { get; set; }
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }
        public int Price { get; set; }
    }

    public class ActorMovie
    {
        public int ActorID { get; set; }
        [ForeignKey("ActorID")]
        public Actor? Actor { get; set; }
        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie? Movie { get; set; }
    }

    public class CustomerFavType
    {
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }
        public int FavMovieTypeID { get; set; }
        [ForeignKey("FavMovieTypeID")]
        public MovieType? MovieType { get; set; }
    }
}
