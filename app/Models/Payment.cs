using app.Data;
using System.Linq;

namespace app.Models
{
    public class Payment
    {
        private ShoppingCart shoppingCart;

        public Payment(ShoppingCart cart)
        {
            shoppingCart = cart;
        }

        public void ApplyDiscount()
        {
            foreach (var product in shoppingCart)
            {
                product.Price *= 0.8;
            }
        }

        public void Execute()
        {
            using (var context = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>()))
            {
                var productsInDb = context.Products
                    .Where(x => shoppingCart
                    .Any(y => y.Id == x.Id));

                foreach (var product in productsInDb)
                {
                    product.Quantity--;
                }
                context.SaveChanges();
            }
        }
    }
}
