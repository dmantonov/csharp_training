using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreation()
        {
            AccountData account = new AccountData
            {
                Username = "administrator",
                Password = "root"
            };

            //формируем список через интерфейс
            //List<ProjectData> oldprojects = new List<ProjectData>();
            //oldprojects = app.Project.GetProjectList();

            //формируем список через апи
            List<ProjectData> oldprojects = new List<ProjectData>(); //Получение списка через API
            oldprojects = app.API.GetProjectListWithAPI(account);

            ProjectData project = new ProjectData
            {
                Name = "New test name s",
                Description = "New test desc s"
            };

            app.Project.ProjectCreation(project);

            //формируем новый список через интерфейс
            //List<ProjectData> newprojects = app.Project.GetProjectList();

            //формируем новый список через апи
            List<ProjectData> newprojects = app.API.GetProjectListWithAPI(account);
            oldprojects.Sort();

            oldprojects.Add(project);

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(newprojects, oldprojects);
        }
    }
}
