using app.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace appTests.Models
{
    [TestFixture]
    public class PaymentTests
    {
        private ShoppingCart cart = new ShoppingCart
        {
            new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
            new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 },
        };

        [TestCase(88.0)]
        public void ApplyDiscountTest(double expectedResult)
        {
            //Arrange
            var payment = new Payment(cart);

            //Act
            payment.ApplyDiscount();

            //Assert
            Assert.AreEqual(expectedResult, cart.Balance);
        }

        [TestCase(70.4)]
        public void ApplyDiscountTwiceTest(double expectedResult)
        {
            //Arrange
            var payment = new Payment(cart);

            //Act
            payment.ApplyDiscount();
            payment.ApplyDiscount();

            //Assert
            Assert.AreEqual(expectedResult, cart.Balance);
        }
    }
}
