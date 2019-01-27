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
            payment = new Payment();
        }

        public void Checkout(User user)
        {
            if (user.IsVip)
                ApplyDiscount();

            if (Balance > user.Balance)
                payment.Execute();
            else
                payment.RollBack();
        }

        public void ApplyDiscount()
        {
            foreach (var product in this)
            {
                product.Price *= 0.8;
            }
        }
    }
}
