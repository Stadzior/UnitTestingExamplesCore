using app.Models;
using NUnit.Framework;

namespace appTests
{
    [TestFixture]
    public class UserTests
    {
        [TestCase]
        public void Given_AverageUser_()
        {
            var user = new User { IsVip = true };
        }
    }
}
