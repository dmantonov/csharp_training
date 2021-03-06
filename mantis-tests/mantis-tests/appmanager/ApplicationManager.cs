﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace mantis_tests
{
    public class ApplicationManager
    {
        public IWebDriver driver;
        protected string baseURL;

        public FtpHelper Ftp { get; set; }
        public JamesHeper James { get; set; }
        public MailHelper Mail { get; set; }

        public LoginHelper loginHelper;

        public AdminHelper Admin { get; set; }
        public APIHelper API { get; private set; }

        protected MantisNavigatorHelper navigator;
        protected ProjectHelper projectHelper;
        protected RegistrationHelper registrationHelper;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/mantisbt-1.2.17";

            registrationHelper = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHeper(this);
            Mail = new MailHelper(this);
            projectHelper = new ProjectHelper(this);
            navigator = new MantisNavigatorHelper(this, baseURL);
            loginHelper = new LoginHelper(this);
            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.Navigator.OpenLoginPage();
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public MantisNavigatorHelper Navigator
        {
            get
            {
                return navigator;
            }
        }
        public ProjectHelper Project
        {
            get
            {
                return projectHelper;
            }
        }

        public RegistrationHelper Registration
        {
            get
            {
                return registrationHelper;
            }
        }
    }
}
