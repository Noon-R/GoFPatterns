using System.Collections.Generic;

namespace GoFPatterns.Iterator.Basic
{
    public class BookShelf : Aggregate
    {
        private List<Book> m_books;

        public BookShelf(List<Book> books)
        {
            m_books = books;
        }

        public void AppendBook(Book book)
        {
            if (m_books.Contains(book)){
                return;
            }

            m_books.Add(book);
        }

        public void Remove(Book book) {
            if (!m_books.Contains(book))
            {
                return;
            }

            m_books.Remove(book);
        }

        public int GetMaxIndex()
        {
            return m_books.Count;
        }

        public Book GetBook(int index) {
            return m_books[index];
        }

        public Iterator iterator()
        {
            return new BookShelfIterator(this);
        }
    }
}
