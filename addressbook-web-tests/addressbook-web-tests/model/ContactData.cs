using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
    {
        private string firstname;
        private string lastname;

        public ContactData(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
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
            return Firstname.GetHashCode()^Lastname.GetHashCode(); //объединение двух параметров
        }

        //перевод в строку, вывод ошибок
        public override string ToString()
        {
            return "Firstname = " + Firstname + ", Lastname = " + Lastname;
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
            return names.CompareTo(othernames) ;
        }

        public string Firstname
        {
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }
    }
}
