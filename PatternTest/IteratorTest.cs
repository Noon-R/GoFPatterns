using System.Collections.Generic;
using Xunit;

using GoFPatterns.Iterator.Basic;

namespace PatternTest
{
    public class IteratorTest
    {
        [Fact]
        public void IteratorBasicTest()
        {
            List<Book> books = new List<Book>();
            BookShelf bookshelf = new BookShelf(books);


            Book[] sampleBooks = {
                new Book("いかにして問題をとくか"),
                new Book("リボーン"),
                new Book("良いコード・悪いコード"),
                new Book("ワンピース")
            };

            bookshelf.AppendBook(sampleBooks[0]);
            bookshelf.AppendBook(sampleBooks[1]);
            bookshelf.AppendBook(sampleBooks[2]);
            bookshelf.AppendBook(sampleBooks[3]);

            Assert.Equal(4,bookshelf.GetMaxIndex());

            int i = 0;
            var it = bookshelf.iterator();
            while (it.hasNext()) {
                var aBook = it.next();
                Assert.Equal(sampleBooks[i],aBook);
                i++;
            }
        }
    }
}
