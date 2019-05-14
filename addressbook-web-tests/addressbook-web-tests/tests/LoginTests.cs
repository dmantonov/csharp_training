using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class LoginTests : TestBase
    {
        [Test]
        public void LoginWithValidCredentials()
        {
            //подготовка
            app.Auth.Logout();

            //действие
            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);

            //проверка
            Assert.IsTrue(app.Auth.IsLoggedIn(account));
        }

        [Test]
        public void LoginWithInvalidCredentials()
        {
            //подготовка
            app.Auth.Logout();

            //действие
            AccountData account = new AccountData("admin", "123456");
            app.Auth.Login(account);

            //проверка
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}
