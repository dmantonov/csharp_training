using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleBrowser.WebDriver;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public Mantis.ProjectData[] projectArray;

        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Username, account.Password, issue);
        }

        public void ProjectCreationWithAPI(AccountData account, ProjectData project)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData projectAPI = new Mantis.ProjectData();
            projectAPI.name = project.Name;


            client.mc_project_add(account.Username, account.Password, projectAPI);
        }

        public List<ProjectData> GetProjectListWithAPI(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            List<ProjectData> projectList = new List<ProjectData>();

            projectArray = client.mc_projects_get_user_accessible(account.Username, account.Password);

            foreach (Mantis.ProjectData projectData in projectArray)
            {
                ProjectData project = new ProjectData();
                project.Name = projectData.name;
                project.Id = projectData.id;
                project.Description = projectData.description;
                projectList.Add(project);
            }

            return projectList;
        }
    }
}
