using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-1.2.17/login_page.php";
        }

        public void FillAuthForm(AccountData admin)
        {
            driver.FindElement(By.Name("username")).SendKeys(admin.Username);
            driver.FindElement(By.Name("password")).SendKeys(admin.Password);
            driver.FindElement(By.CssSelector("input[class = 'button'][value = 'Login']")).Click();
        }

        public void Logout()
        {
            driver.FindElement(By.CssSelector("a[href = '/mantisbt-1.2.17/logout_page.php']")).Click();
        }
        public void Login(AccountData account)
        {
            Type(By.Name("username"), account.Username);
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }
    }
}
