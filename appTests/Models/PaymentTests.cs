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

        [TestCase]
        public void CtorTest()
        {
            //Arrange
            //Act
            var payment = new Payment(cart);
            
            //Assert
            var actualCart = payment.GetType().GetField("shoppingCart", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(payment);
            Assert.AreSame(cart, actualCart);
        }

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
