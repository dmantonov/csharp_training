using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
            this.manager = manager;
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToContactCreationPage();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage();
            return this;
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            ClearGroupFilter();
            SelectContactById(contact.Id);
            SelectGroupToAdd(group.Id);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        internal void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();
            SelectFromGroups(group.Id);
            SelectContactById(contact.Id);
            CommitRemovingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public ContactHelper ModifyByIndex(int index, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModificationByIndex(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper ModifyById(ContactData oldData, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModificationById(oldData.Id);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper RemoveByIndex(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContactByIndex(index);
            InitContactRemove();
            SubmitContactRemove();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper RemoveById(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            SelectContactById(contact.Id);
            InitContactRemove();
            SubmitContactRemove();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhone);
            Type(By.Name("mobile"), contact.MobilePhone);
            Type(By.Name("work"), contact.WorkPhone);
            Type(By.Name("email"), contact.Email1);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            return this;
        }

        //кнопка Enter при создании контакта
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        //кнопка возврата на главную после создания контакта
        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SelectContactByIndex(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContactById(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value='" + id + "'])")).Click();
            return this;
        }

        public ContactHelper SubmitContactRemove()
        {
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactRemove()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        //изменение по нажатию на карандашик
        public ContactHelper InitContactModificationByIndex(int index)
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModificationById(string id)
        {
            driver.FindElement(By.XPath("(//a[@href = 'edit.php?id=" + id + "'])")).Click();
            return this;
        }

        public ContactHelper OpenContactDetails(int index)
        {
            driver.FindElement(By.XPath("(//img[@title='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        //проверка наличия контакта
        public bool IsContactCreated(int index)
        {
            return IsElementPresent(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]"));
        }

        //создаем тестовый контакт, если еще нет созданного
        public void CreateIfContactNotCreated(int index)
        {
            manager.Navigator.GoToHomePage();
            if (!IsContactCreated(index))
            {
                for (int i = 0; i < index + 1; i++)
                {
                    ContactData defaultContactData = new ContactData("Default Firstname " + i, "Default Lastname " + i)
                    {
                        Address = "Default Address",
                        HomePhone = "+7(981)325 56 89",
                        MobilePhone = "+7(555)333 56 89",
                        WorkPhone = "89160989889",
                        Email1 = "defmail1@test.ru",
                        Email2 = "defmail2@test25.ru",
                        Email3 = "defmail3@mail.com"
                    };
                    Create(defaultContactData);
                }
            }
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToHomePage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']")); //создаем список по классу

                foreach (IWebElement element in elements)
                {
                    IWebElement firstnames = element.FindElement(By.CssSelector("td:nth-child(3)")); //забираем имя
                    IWebElement lastnames = element.FindElement(By.CssSelector("td:nth-child(2)")); //забираем фамилию

                    contactCache.Add(new ContactData(firstnames.Text, lastnames.Text) {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }

            return new List<ContactData>(contactCache);
        }
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        public ContactData GetContactInformaionFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList <IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails
            };
        }

        public ContactData GetContactInformaionFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModificationByIndex(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email1 = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Email1 = email1,
                Email2 = email2,
                Email3 = email3
            };
        }

        public ContactData GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            OpenContactDetails(index);
            string allInfo = driver.FindElement(By.CssSelector("div#content")).Text;
            return new ContactData(allInfo)
            {
                AllInfo = allInfo
            };
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d+").Match(text);
            return Int32.Parse(m.Value);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public void SelectGroupToAdd(string id)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByValue(id);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectFromGroups(string id)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByValue(id);
        }

        public void CommitRemovingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void CheckContactWithoutGroup(ContactData contactToAdd, GroupData group)
        {
            if (contactToAdd == null)
            {
                ContactData contact = ContactData.GetAll().First();
                RemoveContactFromGroup(contact, group);
            }
        }

        public void CheckContactWithGroup(ContactData contactToBeRemoved, List<ContactData> oldList, GroupData group)
        {
            if (contactToBeRemoved == null)
            {
                ContactData contact = ContactData.GetAll().First();
                AddContactToGroup(contact, group);
            }
        }
    }
}
