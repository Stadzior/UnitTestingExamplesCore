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
        private static object[] _applyDiscountSource =
        {
            new TestCaseData(new ShoppingCart(), 0.0).SetName("ApplyDiscount_NoItem_BalanceIsEqualTo0"),
            new TestCaseData(new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 }
            }, 88.0).SetName("ApplyDiscount_TwoItemsFor110_BalanceIsEqualTo88")
        };

        [TestCaseSource(nameof(_applyDiscountSource))]
        public void ApplyDiscountTest(ShoppingCart cart, double expectedResult)
        {
            //Arrange
            var payment = new Payment(cart);

            //Act
            payment.ApplyDiscount();

            //Assert
            Assert.AreEqual(expectedResult, cart.Balance);
        }

        private static object[] _applyDiscountTwiceSource =
        {
            new TestCaseData(new ShoppingCart(), 0.0).SetName("ApplyDiscount_NoItem_BalanceIsEqualTo0"),
            new TestCaseData(new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 }
            }, 70.4).SetName("ApplyDiscount_TwoItemsFor110_BalanceIsEqualTo70.4")
        };

        [TestCaseSource(nameof(_applyDiscountTwiceSource))]
        public void ApplyDiscountTwiceTest(ShoppingCart cart, double expectedResult)
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
