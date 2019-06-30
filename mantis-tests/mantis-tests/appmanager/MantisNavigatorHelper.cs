using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class MantisNavigatorHelper : HelperBase
    {
        private string baseURL;

        public MantisNavigatorHelper(ApplicationManager manager, string baseURL) : base(manager)
        {
            this.manager = manager;
            this.baseURL = baseURL;
        }

        public void OpenManagePage()
        {
            driver.FindElement(By.CssSelector("a[href = '/mantisbt-1.2.17/manage_overview_page.php']")).Click();
        }

        public void OpenManageProjectsPage()
        {
            driver.FindElement(By.CssSelector("a[href = '/mantisbt-1.2.17/manage_proj_page.php']")).Click();
        }

        public void OpenLoginPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/mantisbt-1.2.17/login_page.php");
        }
    }
}
