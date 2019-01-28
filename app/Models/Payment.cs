using app.Data;
using System.Linq;

namespace app.Models
{
    public class Payment
    {
        public ShoppingCart ShoppingCart { get; set; }

        public Payment(ShoppingCart cart)
        {
            ShoppingCart = cart;
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
            using (var context = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>()))
            {
                var productsInDb = context.Products
                    .Where(x => ShoppingCart
                    .Any(y => y.Id == x.Id));

                foreach (var product in productsInDb)
                {
                    product.Quantity--;
                }
                context.SaveChanges();
            }
        }

        public double GetProductQuantity(int productId)
        {
            using (var context = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>()))
            {
                return context.Products.Single(x => x.Id == productId).Quantity;
            }
        }
    }
}
