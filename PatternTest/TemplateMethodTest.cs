using System.Collections.Generic;
using Xunit;

using GoFPatterns.TemplateMethod.Example;

namespace PatternTest
{
    public class TemplateMethodTest
    {
        [Fact]
        public void CharDisplayMethodTest() {

            CharDisplay cDisplay = new CharDisplay('N');
            string displayText = cDisplay.Display();
            Assert.Equal("<<NNNNN>>\r\n",displayText);
        }

        [Fact]
        public void StringDisplayMethodTest()
        {

            StringDisplay cDisplay = new StringDisplay("Hello");
            string displayText = cDisplay.Display();
            Assert.Equal("+-----+\r\n|Hello|\r\n|Hello|\r\n|Hello|\r\n|Hello|\r\n|Hello|\r\n+-----+\r\n", displayText);
        }
    }
}
