using System.Collections.Generic;
using Xunit;

using GoFPatterns.Generate.FactoryMethod;
using GoFPatterns.Generate.FactoryMethod.example;

namespace PatternTest
{
    public class FactoryMethodTest
    {
        [Fact]
        public void BasicTest()
        {
            AFactory factory = new IDCardFactory();

            IProduct product = factory.Create("Noon");

            Assert.Equal("Noon", product.use());
        }
    }
}
