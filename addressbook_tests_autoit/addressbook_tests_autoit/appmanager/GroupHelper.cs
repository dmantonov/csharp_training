using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace addressbook_tests_autoit
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPEDITWINTITLE = "Group editor";
        public static string GROUPDELETEWINTITLE = "Delete group";

        public GroupHelper(ApplicationManager manager) : base(manager) { }

        public List<GroupData> GetGroupList()
        {
            List<GroupData> list = new List<GroupData>();
            OpenGroupsDialog();
            string count = aux.ControlTreeView(GROUPEDITWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetItemCount", "#0", "");
            for (int i = 0; i < int.Parse(count); i++)
            {
                string item = aux.ControlTreeView(GROUPEDITWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "GetText", "#0|#" + i, "");
                list.Add(new GroupData()
                {
                    Name = item
                });
            }
            CloseGroupsDialog();
            return list;
        }

        public void Add(GroupData newGroup)
        {
            OpenGroupsDialog();
            aux.ControlClick(GROUPEDITWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.Send(newGroup.Name);
            aux.Send("{ENTER}");
            CloseGroupsDialog();
        }

        public void Remove(int index)
        {
            OpenGroupsDialog();
            GroupSelection(index);
            DeleteGroup();
            SubmitGroupDelete();
            CloseGroupsDialog();
        }

        public void CloseGroupsDialog()
        {
            aux.ControlClick(GROUPEDITWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d54");
        }

        public void OpenGroupsDialog()
        {
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d512");
            aux.WinWait(GROUPEDITWINTITLE);
        }

        private void GroupSelection(int i)
        {
            aux.ControlTreeView(GROUPEDITWINTITLE, "", "WindowsForms10.SysTreeView32.app.0.2c908d51", "Select", "#0|#" + i, "");
        }

        public void SubmitGroupDelete()
        {
            aux.ControlClick(GROUPDELETEWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d53");
            aux.WinWait(GROUPEDITWINTITLE);
        }

        public void DeleteGroup()
        {
            aux.ControlClick(GROUPEDITWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d51");
            aux.WinWait(GROUPDELETEWINTITLE);
        }

        public void CreateIfGroupNotCreated(List<GroupData> oldGroups, GroupData newGroup)
        {
            if (oldGroups.Count == 1)
            {
                Add(newGroup);
            }
        }
    }
}