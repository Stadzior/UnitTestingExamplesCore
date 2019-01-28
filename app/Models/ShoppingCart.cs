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
        public Payment Payment { get; set; }

        IOutputService OutputService { get; set; }

        public ShoppingCart()
        {
            OutputService = OutputService ?? new ConsoleOutputService();
        }

        public void Checkout(Customer customer)
        {
            if (customer.IsVip)
                Payment.ApplyDiscount();

            if (Balance > customer.Balance)
            {
                Payment.Execute();
                customer.Balance -= Balance;
                Clear();
            }
            else
                OutputService.WriteLine("No money, no deal.");
        }
    }
}
