using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class ProjectCreatonTests : AuthTestBase
    {
        [Test]
        public void ProjectCreation()
        {
            ProjectData project = new ProjectData
            {
                Name = "Test Project2",
                Description = "Test Description2"
            };

            app.Project.ProjectCreation(project);
        }
    }
}
