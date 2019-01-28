using app.Models;
using app.Services;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace appTests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        [TestCase]
        public void AverageCustomerCheckoutTest()
        {
            //Arrange
            var customerPartialMock = new Mock<Customer>
            {
                CallBase = true
            };

            var outputServiceMock = new Mock<IOutputService>();
            customerPartialMock.Setup(x => x.OutputService).Returns(outputServiceMock.Object);

            customerPartialMock.Setup(x => x.IsVip).Returns(false);
            customerPartialMock.Protected().Setup("ShoppingCartCheckout");

            //Act
            customerPartialMock.Object.Checkout();

            //Assert
            customerPartialMock.Protected().Verify("ShoppingCartCheckout", Times.Once());
            outputServiceMock.Verify(x => x.WriteLine("You're V.I.P"), Times.Never());
        }

        [TestCase]
        public void VipCustomerTest()
        {
            //Arrange
            var customerPartialMock = new Mock<Customer>
            {
                CallBase = true
            };

            var outputServiceMock = new Mock<IOutputService>();
            customerPartialMock.Setup(x => x.OutputService).Returns(outputServiceMock.Object);

            customerPartialMock.Setup(x => x.IsVip).Returns(true);
            customerPartialMock.Protected().Setup("ShoppingCartCheckout");

            //Act
            customerPartialMock.Object.Checkout();

            //Assert
            customerPartialMock.Protected().Verify("ShoppingCartCheckout", Times.Once());
            outputServiceMock.Verify(x => x.WriteLine("You're V.I.P"), Times.Once());
        }
    }
}
