using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Dmitry", "Antonov");
            contact.Address = "rggrg\r\nfdgd\r\n\r\ndfgdf";
            contact.HomePhone = "+7(916) 1 - 2 - 3";
            contact.MobilePhone = "+7 (916) 4 5 6";
            contact.Email1 = "test1@test.ru";
            contact.Email3 = "test-em@mail.com";

            List<ContactData> oldContacts = app.Contacts.GetContactList();

            app.Contacts.Create(contact);

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactCount());

            List<ContactData> newContacts = app.Contacts.GetContactList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
