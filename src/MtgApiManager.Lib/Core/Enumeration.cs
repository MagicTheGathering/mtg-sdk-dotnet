using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MtgApiManager.Lib.Core
{
    internal abstract class Enumeration : IComparable
    {
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields
            .Select(f => f.GetValue(null))
            .Cast<T>();
        }

        public int CompareTo(object obj) => Id.CompareTo(((Enumeration)obj).Id);

        public override bool Equals(object obj)
        {
            if (obj is Enumeration otherValue)
            {
                return GetType().Equals(obj.GetType())
                    && Id.Equals(otherValue.Id);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 1460282102;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            return hashCode;
        }

        public override string ToString() => Name;
    }
}