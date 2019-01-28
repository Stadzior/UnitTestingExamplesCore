using app.Models;
using app.Services;
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

        IOutputService OutputService { get; set; }

        public ShoppingCart()
        {
            OutputService = OutputService ?? new ConsoleOutputService();
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
                OutputService.WriteLine("No money, no deal.");
        }
    }
}
