using app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app.Models
{
    public class ShoppingCart : List<Product>
    {
        public double Balance => this.Sum(x => x.Price);

        private Payment payment;
        
        public ShoppingCart()
        {
            payment = new Payment(this);
        }

        public void Checkout(Customer user)
        {
            if (user.IsVip)
                payment.ApplyDiscount();

            if (Balance > user.Balance)
            {
                payment.Execute();
                user.Balance -= Balance;
                Clear();
            }
            else
                Console.WriteLine("No money, no deal.");
        }
    }
}
