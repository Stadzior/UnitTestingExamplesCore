using app.Services;

namespace app.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
        public bool IsVip { get; set; }

        private ShoppingCart shoppingCart;

        public User()
        {
            shoppingCart = new ShoppingCart();
        }

        public void Checkout()
        {
            shoppingCart.Checkout(this);
        }
    }
}
