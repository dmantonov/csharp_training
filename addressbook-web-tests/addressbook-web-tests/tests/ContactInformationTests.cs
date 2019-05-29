using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactHomePageInformation()
        {
            app.Contacts.CreateIfContactNotCreated(0);

            ContactData fromTable = app.Contacts.GetContactInformaionFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformaionFromEditForm(0);

            //проверки
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }

        [Test]
        public void TestContactDetailsInformation()
        {
            app.Contacts.CreateIfContactNotCreated(0);

            ContactData fromDetails = app.Contacts.GetContactInformationFromDetails(0);
            ContactData fromForm = app.Contacts.GetContactInformaionFromEditForm(0);

            //проверки
            Assert.AreEqual(fromDetails.AllInfo, fromForm.AllInfo);
        }
    }
}
