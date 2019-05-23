using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        public ContactHelper Modify(int index, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(index);
            InitContactRemove();
            SubmitContactRemove();
            manager.Navigator.GoToHomePage();
            return this;

        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
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

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
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
        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@title='Edit'])[" + (index+1) + "]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        //проверка наличия конакта
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
                    ContactData defaultContactData = new ContactData("Default Firstname " + i, "Default Lastname " + i);
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
    }
}
