using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace mantis_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }

        public void ProjectCreation(ProjectData project)
        {
            manager.Navigator.OpenManagePage();
            manager.Navigator.OpenManageProjectsPage();
            InitProjectCreation();
            FillProjectRegistrtionForm(project);
            SubmitAddingProject();
            //Continue();
        }

        private void Continue()
        {
            driver.FindElement(By.CssSelector("a[href = 'manage_proj_page.php']")).Click();
        }

        private void SubmitAddingProject()
        {
            driver.FindElement(By.CssSelector("input[type='submit'][value='Add Project']")).Click();
        }

        private void FillProjectRegistrtionForm(ProjectData project)
        {
            driver.FindElement(By.Name("name")).SendKeys(project.Name);
            driver.FindElement(By.Name("description")).SendKeys(project.Description);
        }

        private void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input[type='submit'][value='Create New Project']")).Click();
        }
    }
}
