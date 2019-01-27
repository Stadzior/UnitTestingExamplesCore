using app.Models;
using NUnit.Framework;

namespace appTests.Models
{
    [TestFixture]
    public class UserTests
    {
        [TestCase]
        public void AverageUserTest()
        {
            var user = new Customer { IsVip = false };
        }

        [TestCase]
        public void VipUserTest()
        {
            var user = new Customer { IsVip = true };
        }
    }
}
