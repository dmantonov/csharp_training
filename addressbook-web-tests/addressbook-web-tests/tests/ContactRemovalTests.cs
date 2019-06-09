using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            //проверка на наличие контакта
            app.Contacts.CreateIfContactNotCreated(0);

            //создаем список со старыми данными и удаляем контакт
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contacts.RemoveById(toBeRemoved);

            //осуществляем проверки, что удалился старый контакт
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData cont in newContacts)
            {
                Assert.AreNotEqual(cont.Id, toBeRemoved.Id);
            }
        }
    }
}
