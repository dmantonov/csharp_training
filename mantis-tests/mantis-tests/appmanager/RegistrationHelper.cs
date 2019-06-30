﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
            string url = GetConfirmationUrl(account);
            FillPassworForm(url);
            SubmitPasswordForm();
        }

        public string GetConfirmationUrl(AccountData account)
        {
            string message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        public void FillPassworForm(string url)
        {
            throw new NotImplementedException();
        }

        public void SubmitPasswordForm()
        {
            throw new NotImplementedException();
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
