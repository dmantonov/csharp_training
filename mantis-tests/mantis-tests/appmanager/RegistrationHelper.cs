using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrtionForm();
            FillRegitrationForm(account);
            SubmitRegistration();
        }

        private void OpenRegistrtionForm()
        {
            driver.FindElement(By.CssSelector("a.back-to-login-link.pull-left")).Click();
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input[type = 'submit']")).Click();
        }

        public void FillRegitrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.21.1/login_page.php";
        }
    }
}
