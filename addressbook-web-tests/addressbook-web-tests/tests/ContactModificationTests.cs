using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Petr", "Ivanov");

            //проверка на наличие контакта
            app.Contacts.CreateIfContactNotCreated(0);

            //создаем список со старыми данными и изменяем контакт
            List<ContactData> oldContacts = app.Contacts.GetContactList();
            ContactData oldData = oldContacts[0];

            app.Contacts.Modify(0, newData);

            //осуществляем проверки, что изменился старый контакт
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData cont in newContacts)
            {
                if (cont.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Firstname, cont.Firstname);
                    Assert.AreEqual(newData.Lastname, cont.Lastname);
                }
            }
        }
    }
}
