using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectRemovingTests : AuthTestBase
    {
        [Test]
        public void ProjectRemoving()
        {

            AccountData account = new AccountData
            {
                Username = "administrator",
                Password = "root"
            };

            //app.Project.CreateIfProjectNotCreated(0);

            //формируем список через интерфейс
            //List<ProjectData> oldprojects = app.Project.GetProjectList();

            //формируем список через апи
            List<ProjectData> oldprojects = new List<ProjectData>();

            oldprojects = app.API.GetProjectListWithAPI(account);
            if (oldprojects.Count == 0)
            {
                ProjectData project = new ProjectData
                {
                    Name = "Default name",
                    Description = "Default description"
                };

                app.API.ProjectCreationWithAPI(account, project);
                oldprojects = app.API.GetProjectListWithAPI(account);
            }

            ProjectData toBeRemoved = oldprojects[0];

            app.Project.ProjectRemoving(0);

            //формируем список через интерфейс
            //List<ProjectData> newprojects = app.Project.GetProjectList();

            //формируем список через апи
            List<ProjectData> newprojects = app.API.GetProjectListWithAPI(account);

            oldprojects.Remove(toBeRemoved);

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(newprojects, oldprojects);
        }
    }
}
