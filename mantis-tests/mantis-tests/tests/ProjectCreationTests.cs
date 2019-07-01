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
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            ProjectData project = new ProjectData
            {
                Name = "New test name s",
                Description = "New test desc s"
            };

            app.Project.ProjectCreation(project);

            List<ProjectData> newprojects = app.Project.GetProjectList();

            oldprojects.Add(project);

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);
        }
    }
}
