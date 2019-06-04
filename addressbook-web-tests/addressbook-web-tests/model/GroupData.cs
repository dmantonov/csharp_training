﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddressbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>  //объекты типа GroupData теперь можно сравнивать и сортировать
    {
        public GroupData()
        {
        }

        public GroupData(string name)
        {
            Name = name;
        }

        //сравниваем объекты, списки целиком
        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name; //сравниваем по имени
        }

        //оптимизация сравнения
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        //перевод в строку, вывод ошибок
        public override string ToString()
        {
            return "name = " + Name + "\nHeader = " + Header + "\nFooter = " + Footer;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public string Name { get; set; }

        public string Header { get; set; }

        public string Footer { get; set; }

        public string Id { get; set; }
    }
}
