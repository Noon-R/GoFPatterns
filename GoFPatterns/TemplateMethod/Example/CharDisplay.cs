using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.TemplateMethod.Example
{
    public class CharDisplay : ADisplay
    {
        char m_char;
        public CharDisplay(char key) {
            m_char = key;
        }

        protected override string Close()
        {
            return ">>";
        }

        protected override string Open()
        {
            return "<<";
        }

        protected override string Print()
        {
            return m_char.ToString();
        }
    }
}
