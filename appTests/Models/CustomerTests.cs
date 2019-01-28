using app.Models;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using System.Reflection;

namespace appTests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        [TestCase]
        public void AverageCustomerCheckoutTest()
        {
            var customerPartialMock = new Mock<Customer>
            {
                CallBase = true
            };

            customerPartialMock.Setup(x => x.IsVip).Returns(false);
            customerPartialMock.Protected().Setup("ShoppingCartCheckout");
            customerPartialMock.Object.Checkout();
            customerPartialMock.Protected().Verify("ShoppingCartCheckout", Times.Once());
        }

        [TestCase]
        public void VipCustomerTest()
        {
            var customerPartialMock = new Mock<Customer>
            {
                CallBase = true
            };

            customerPartialMock.Setup(x => x.IsVip).Returns(true);
            customerPartialMock.Protected().Setup("ShoppingCartCheckout");
            customerPartialMock.Object.Checkout();
            customerPartialMock.Protected().Verify("ShoppingCartCheckout", Times.Once());
        }
    }
}
