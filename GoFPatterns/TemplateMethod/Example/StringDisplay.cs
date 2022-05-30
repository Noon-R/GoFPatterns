using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.TemplateMethod.Example
{
    public class StringDisplay : ADisplay
    {
        string m_text;

        public StringDisplay(string text) {
            m_text = text;
        }

        protected override string Close()
        {
            StringBuilder stringBuilder = GetLine();
            return stringBuilder.ToString();
        }

        protected override string Open()
        {
            StringBuilder stringBuilder = GetLine();
            stringBuilder.AppendLine("");
            return stringBuilder.ToString();
        }

        protected override string Print()
        {
            StringBuilder stringBuilder = new StringBuilder("|");
            stringBuilder.Append(m_text);
            stringBuilder.AppendLine("|");

            return stringBuilder.ToString();
        }

        private StringBuilder GetLine()
        {
            StringBuilder stringBuilder = new StringBuilder("+");
            for (int i = 0; i < m_text.Length; i++)
            {
                stringBuilder.Append('-');
            }
            stringBuilder.Append('+');
            return stringBuilder;
        }

    }
}
