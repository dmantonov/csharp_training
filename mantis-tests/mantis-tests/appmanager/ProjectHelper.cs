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
            Continue();
        }

        public void ProjectRemoving(int index)
        {
            manager.Navigator.OpenManagePage();
            manager.Navigator.OpenManageProjectsPage();
            SelectProject(index);
            DeleteProject();
            SubmitProjectDeletion();
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

        public List<ProjectData> GetProjectList()
        {
            manager.Navigator.OpenManagePage();
            manager.Navigator.OpenManageProjectsPage();

            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElement(By.XPath("/html/body/table[3]/tbody"))
                .FindElements(By.XPath(".//tr[@class='row-1' or @class='row-2']"));

            foreach (IWebElement element in elements)
                {
                    IWebElement name = element.FindElement(By.XPath(".//td[1]")); //забираем название проекта
                    IWebElement decription = element.FindElement(By.XPath(".//td[5]")); //забираем дескрипшен

                    projects.Add(new ProjectData(name.Text, decription.Text));
                }
                return new List<ProjectData>(projects);
        }
        public void SelectProject(int index)
        {
            driver.FindElement(By.XPath("/html/body/table[3]/tbody/tr[" + (index+3) + "]/td[1]/a")).Click();
        }

        private void DeleteProject()
        {
            driver.FindElement(By.CssSelector("input[type='submit'][value='Delete Project']")).Click();
        }

        private void SubmitProjectDeletion()
        {
            driver.FindElement(By.CssSelector("input[type='submit'][value='Delete Project']")).Click();
        }

        public bool IsProjectCreated(int index)
        {
            return IsElementPresent(By.XPath("/html/body/table[3]/tbody/tr[" + (index + 3) + "]"));
        }

        public void CreateIfProjectNotCreated(int index)
        {
            manager.Navigator.OpenManagePage();
            manager.Navigator.OpenManageProjectsPage();
            if (!IsProjectCreated(index))
            {
                for (int i = 0; i < index + 1; i++)
                {
                    ProjectData defaultProjectData = new ProjectData("Default Name", "Default Description");
                    ProjectCreation(defaultProjectData);
                }
            }
        }
    }
}
