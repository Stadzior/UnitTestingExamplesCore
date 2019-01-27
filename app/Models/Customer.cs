﻿using app.Services;
using System;

namespace app.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
        public bool IsVip { get; set; }

        private ShoppingCart shoppingCart;

        public Customer()
        {
            shoppingCart = new ShoppingCart();
        }

        public void Checkout()
        {
            if (IsVip)
                Console.WriteLine("You're V.I.P");
            //Some complex logic
            shoppingCart.Checkout(this);
        }
    }
}