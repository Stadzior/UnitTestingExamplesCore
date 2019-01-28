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
        [TestCase(true, 1, TestName = "Checkout_VipCustomer_CheckoutPerformedAndMessageDisplayed")]
        [TestCase(false, 0, TestName = "Checkout_AverageCustomer_CheckoutPerformedAndMessageNotDisplayed")]
        public void AverageCustomerCheckoutTest(bool isVip, int expectedMessageDisplayCount)
        {
            //Arrange
            var customerPartialMock = new Mock<Customer>
            {
                CallBase = true
            };

            var outputServiceMock = new Mock<IOutputService>();
            customerPartialMock.Setup(x => x.OutputService).Returns(outputServiceMock.Object);

            customerPartialMock.Setup(x => x.IsVip).Returns(isVip);
            customerPartialMock.Protected().Setup("ShoppingCartCheckout");

            //Act
            customerPartialMock.Object.Checkout();

            //Assert
            customerPartialMock.Protected().Verify("ShoppingCartCheckout", Times.Once());
            outputServiceMock.Verify(x => x.WriteLine("You're V.I.P"), Times.Exactly(expectedMessageDisplayCount));
        }
    }
}
