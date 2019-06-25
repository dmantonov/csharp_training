using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class RemoveContactFromGroupTests : AuthTestBase
    {
        [Test]
        public void RemoveContactFromGroups()
        {
            //проверка наличия группы и контакта
            app.Groups.CreateIfGroupNotCreated(0);

            app.Contacts.CreateIfContactNotCreated(0);

            //подготовка данных
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = oldList.FirstOrDefault();

            app.Contacts.CheckContactWithGroup(contact, oldList, group);

            if (contact == null)
            {
                contact = group.GetContacts().FirstOrDefault();
            }

            //действия
            app.Contacts.RemoveContactFromGroup(contact, group);

            //проверки
            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
