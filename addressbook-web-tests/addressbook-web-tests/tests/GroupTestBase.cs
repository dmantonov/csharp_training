using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class GroupTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareGroupsUi_DB()
        {
            if (Perform_Long_UI_Checks)
            {
                List<GroupData> fromUi = app.Groups.GetGroupList();
                List<GroupData> fromDB = GroupData.GetAll();
                fromUi.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUi, fromDB);
            }
        }
    }
}
