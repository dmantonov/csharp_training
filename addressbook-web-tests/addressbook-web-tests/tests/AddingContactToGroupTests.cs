using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddingContactToGroup()
        {
            //проверка наличия группы и контакта
            app.Groups.CreateIfGroupNotCreated(0);

            app.Contacts.CreateIfContactNotCreated(0);

            //подготовка данных
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).FirstOrDefault();

            app.Contacts.CheckContactWithoutGroup(contact, group);

            if (contact == null)
            {
                oldList = group.GetContacts();
                contact = ContactData.GetAll().Except(oldList).FirstOrDefault();
            }

            //действия
            app.Contacts.AddContactToGroup(contact, group);
            
            //проверки
            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
