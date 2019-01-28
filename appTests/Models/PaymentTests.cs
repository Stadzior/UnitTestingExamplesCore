using app.Data;
using app.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private static object[] _executeSource =
        {
            new TestCaseData(new List<Product>
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 }
            }, new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 }
            }, new List<Product>
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 1, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 0 }
            }, new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 1, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 0 }
            }).SetName("Execute_TwoItemsWithQuantity2And1_TwoItemsWithQuantity1And0"),
            new TestCaseData(new List<Product>
            {
                new Product { Id = 3, Name = "NotFunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 4, Name = "NotFunnyAtAllItem", Price = 10.0, Quantity = 1 }
            }, new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 1 }
            }, new List<Product>
            {
                new Product { Id = 3, Name = "NotFunnyItem", Price = 100.0, Quantity = 2, },
                new Product { Id = 4, Name = "NotFunnyAtAllItem", Price = 10.0, Quantity = 1 }
            }, new ShoppingCart
            {
                new Product { Id = 1, Name = "FunnyItem", Price = 100.0, Quantity = 1, },
                new Product { Id = 2, Name = "LessFunnyItem", Price = 10.0, Quantity = 0 }
            }).SetName("Execute_TwoItemsNotPresentInDb_QuantityNotDecreasedOnDb")
        };

        [TestCaseSource(nameof(_executeSource))]
        public void ExecuteTest(List<Product> productsInDb, ShoppingCart cart, List<Product> expectedDbResult, ShoppingCart expectedCart)
        {
            //Arrange
            var applicationDbContextMock = new Mock<IApplicationDbContext>();
            applicationDbContextMock.SetupEntityCollection(x => x.Products, productsInDb);
            var applicationDbContextFactoryMock = new Mock<IApplicationDbContextFactory>();
            applicationDbContextFactoryMock.Setup(x => x.Create(It.IsAny<IDbContextOptions>())).Returns(applicationDbContextMock.Object);

            var payment = new Payment(cart)
            {
                ApplicationDbContextFactory = applicationDbContextFactoryMock.Object
            };

            //Act
            payment.Execute();

            //Assert
            Assert.That(productsInDb.Count == expectedDbResult.Count &&
                        productsInDb.All(x => expectedDbResult.Any(y =>
                                      x.Id == y.Id &&
                                      x.Name == y.Name &&
                                      x.Price == y.Price &&
                                      x.Quantity == y.Quantity)) &&
                        cart.Count == expectedCart.Count &&
                        cart.All(x => expectedCart.Any(y =>
                                      x.Id == y.Id &&
                                      x.Name == y.Name &&
                                      x.Price == y.Price &&
                                      x.Quantity == y.Quantity)));
        }
    }
}
