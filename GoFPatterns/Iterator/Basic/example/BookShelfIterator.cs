namespace GoFPatterns.Iterator.Basic
{
    class BookShelfIterator : Iterator
    {
        private readonly BookShelf m_bookShelf;
        private int topIndex = 0;

        public BookShelfIterator(BookShelf shelf) {
            topIndex = 0;
            m_bookShelf = shelf;
        }

        public bool hasNext()
        {
            return topIndex < m_bookShelf.GetMaxIndex();
        }

        public object next()
        {
            var book = m_bookShelf.GetBook(topIndex);
            topIndex++;
            return book;
        }
    }
}
