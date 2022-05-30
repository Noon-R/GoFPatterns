using System.Collections.Generic;
using Xunit;

using GoFPatterns.Adapter.example;

namespace PatternTest
{
    public class AdapterTest
    {
        [Fact]
        public void BasicTest() {
            IPrint printer = new PrintBarnner("テスト");

            Assert.Equal("(テスト)", printer.PrintWeak());
            Assert.Equal("*テスト*", printer.PrintStrong());
        }
    }
}
