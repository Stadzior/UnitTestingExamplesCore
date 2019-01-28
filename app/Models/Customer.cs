using app.Services;
using System;

namespace app.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
        public virtual bool IsVip { get; set; }

        public virtual IOutputService OutputService { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public Customer()
        {
            OutputService = OutputService ?? new ConsoleOutputService();
        }

        public void Checkout()
        {
            if (IsVip)
                OutputService.WriteLine("You're V.I.P");
            ShoppingCartCheckout();
        }

        protected virtual void ShoppingCartCheckout()
        {
            ShoppingCart.Checkout(this);
        }
    }
}
