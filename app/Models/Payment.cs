using app.Data;
using System.Linq;

namespace app.Models
{
    public class Payment
    {
        public ShoppingCart ShoppingCart { get; set; }

        public IApplicationDbContextFactory ApplicationDbContextFactory { get; set; }

        public Payment(ShoppingCart cart)
        {
            ShoppingCart = cart;
            ApplicationDbContextFactory = ApplicationDbContextFactory ?? new ApplicationDbContextFactory();
        }

        public void ApplyDiscount()
        {
            foreach (var product in ShoppingCart)
            {
                product.Price *= 0.8;
            }
        }

        public void Execute()
        {
            using (var context = ApplicationDbContextFactory.Create())
            {
                var productsInDb = context.Products
                    .Where(x => ShoppingCart
                    .Any(y => y.Id == x.Id));

                foreach (var productId in ShoppingCart.Select(x => x.Id))
                {
                    ShoppingCart.Single(x => x.Id == productId).Quantity--;

                    var productInDb = productsInDb.SingleOrDefault(x => x.Id == productId);
                    if (productInDb != null)
                        productInDb.Quantity--;
                }
                context.SaveChanges();
            }
        }

        public double GetProductQuantity(int productId)
        {
            using (var context = ApplicationDbContextFactory.Create())
            {
                return context.Products.Single(x => x.Id == productId).Quantity;
            }
        }
    }
}
