﻿using System.ComponentModel.DataAnnotations.Schema;


namespace PatikaFinalProject.DataAccess
{
    public class ShoppingList
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public virtual Category? Category { get; set; }
        public virtual List<Product>? ProductList { get; set; }
        
        public bool isBought { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime CompletedDate { get; set; }
    }

    public class Category
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

    public class Product
    {
        public int ID { get; set; }
        public int ShoppingListID { get; set; }
        public string? Name { get; set; }
        public string? Unit { get; set; }
        public int Amount { get; set; }
    }

    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public byte[]? HashedPass { get; set; }
        public string UserType { get; set; } // Member, Admin

        public User(string userName, byte[] hashedPass, string userType)
        {
            UserName = userName;
            HashedPass = hashedPass;
            UserType = userType;
        }
        public User(string userName, string userType)
        {
            UserName = userName;
            UserType = userType;
        }
    }
}
