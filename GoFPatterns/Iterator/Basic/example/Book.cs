using System;
using System.Collections.Generic;

namespace GoFPatterns.Iterator.Basic
{
    public class Book : IEquatable<Book>
    {
        private readonly string m_name;

        public Book(in string name) {
            m_name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Book);
        }

        public bool Equals(Book other)
        {
            return other != null &&
                   m_name == other.m_name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(m_name);
        }

        public ref readonly string GetName(){
            return ref m_name;
        }

        public static bool operator ==(Book left, Book right)
        {
            return EqualityComparer<Book>.Default.Equals(left, right);
        }

        public static bool operator !=(Book left, Book right)
        {
            return !(left == right);
        }
    }
}
