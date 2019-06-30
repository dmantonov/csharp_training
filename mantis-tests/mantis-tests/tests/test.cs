using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class test : TestBase
    {
        [Test]
        public void Test()
        {
            app.Navigator.OpenManagePage();
            app.Navigator.OpenManageProjectsPage();
        }

        [OneTimeTearDown]
        public void Logout()
        {
            app.Auth.Logout();
        }
    }
}
