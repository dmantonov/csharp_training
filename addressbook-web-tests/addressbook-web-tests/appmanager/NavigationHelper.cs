using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class NavigationHelper : HelperBase
    {
        private string baseURL;

        public NavigationHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.manager = manager;
            this.baseURL = baseURL;
        }

        //страница логина
        public void OpenLoginPage()
        {
            if (driver.Url == baseURL + "/addressbook/"
                && IsElementPresent(By.Name("LoginForm")))
            {
                return;
            }
                driver.Navigate().GoToUrl(baseURL + "/addressbook/");
        }

        public void GoToGroupsPage()
        {
            if (driver.Url == baseURL + "/addressbook/group.php"
                && IsElementPresent(By.Name("new")))
            {
                return;
            }
            driver.FindElement(By.LinkText("groups")).Click();
        }

        //стартовая страница
        public void GoToHomePage()
        {
            if (driver.Url == baseURL + "/addressbook/"
                && IsElementPresent(By.Name("logout")))
            {
                return;
            }
            driver.FindElement(By.LinkText("home")).Click();
        }

        public void GoToContactCreationPage()
        {
            if (driver.Url == baseURL + "/addressbook/edit.php"
                && IsElementPresent(By.Name("submit")))
            {
                return;
            }
            driver.FindElement(By.LinkText("add new")).Click();
        }
    }
}
