using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    [Table(Name = "addressbook")] //таблица для контактов
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string allPhones;
        private string allEmails;
        private string allInfo;

        public ContactData(string allInfo)
        {
            AllInfo = allInfo;
        }

        public ContactData()
        {
        }

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        //сравниваем объекты, списки целиком
        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Firstname == other.Firstname && Lastname == other.Lastname; //сравниваем по имени и фамилии
        }

        //оптимизация сравнения
        public override int GetHashCode()
        {
            return Firstname.GetHashCode() ^ Lastname.GetHashCode(); //объединение двух параметров
        }

        //перевод в строку, вывод ошибок
        public override string ToString()
        {
            return "Firstname = " + Firstname + "\nLastname = " + Lastname
                + "\nAddress = " + Address
                + "\nHomePhone = " + HomePhone + "\nMobilePhone = " + MobilePhone + "\nWorkPhone = " + WorkPhone
                + "\nEmail1 = " + Email1 + "\nEmail2 = " + Email2 + "\nEmail3 = " + Email3;
        }

        //операция сравнениия
        public int CompareTo(ContactData other)
        {
            string othernames = other.Firstname + other.Lastname;
            string names = Firstname + Lastname;
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return names.CompareTo(othernames);
        }

        [Column(Name = "firstname")]
        public string Firstname { get; set; }

        [Column(Name = "lastname")]
        public string Lastname { get; set; }

        [Column(Name = "id"), PrimaryKey, Identity]
        public string Id { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "email")]
        public string Email1 { get; set; }

        [Column(Name = "email2")]
        public string Email2 { get; set; }

        [Column(Name = "email3")]
        public string Email3 { get; set; }

        [Column(Name = "deprecated")] //дата удаления контакта
        public string Deprecated { get; set; }

        [Column(Name = "modified")] //дата модификации контакта
        public string Modified { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (PhoneCleanUp(HomePhone) + PhoneCleanUp(MobilePhone) + PhoneCleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        //не получилось адаптировать для двух тестов, надо подумать
        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (EmailModification(Email1) + EmailModification(Email2) + EmailModification(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string AllInfo
        {
            get
            {
                if (allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    return (Firstname + " " + Lastname + "\r\n" + Address + "\r\n"
                        + PhoneModification(HomePhone, "H") + PhoneModification(MobilePhone, "M") + PhoneModification(WorkPhone, "W") + "\r\n"
                        + EmailModification(Email1) + EmailModification(Email2) + EmailModification(Email3)).Trim();
                }
            }
            set
            {
                allInfo = value;
            }
        }

        public static List<ContactData> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts.Where(x => x.Deprecated == "0000-00-00 00:00:00") //проверяем, что контакт не удален
                        orderby c.Modified ascending //сортируем по дате модификации, чтобы каждый раз модифицировался новый контакт
                        select c).ToList();
            }
        }

        //убираем лишние символы в телефоне
        public string PhoneCleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            else
            {
                return Regex.Replace(phone, "[-( )]", "") + "\r\n";
            }
        }

        //модфицируем телефон для details
        public string PhoneModification(string phone, string type)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            else
            {
                phone = $"\r\n{type}: {phone}";
                return phone;
            }
        }

        //модифицируем e-mail для details
        public string EmailModification(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            else
            {
                email = $"\r\n{email}";
                return email;
            }
        }
    }
}
