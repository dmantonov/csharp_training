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

            app.Project.CreateIfProjectNotCreated(0);

            List<ProjectData> oldprojects = app.Project.GetProjectList();

            ProjectData toBeRemoved = oldprojects[0];

            app.Project.ProjectRemoving(0);

            List<ProjectData> newprojects = app.Project.GetProjectList();

            oldprojects.Remove(toBeRemoved);

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(newprojects, oldprojects);
        }
    }
}
