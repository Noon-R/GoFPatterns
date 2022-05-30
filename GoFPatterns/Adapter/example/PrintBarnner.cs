using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Adapter.example
{
    public class PrintBarnner : IPrint
    {
        private readonly Barnner m_barnner;

        public PrintBarnner(string text) {
            m_barnner = new Barnner(text);
        }

        public string PrintStrong()
        {
            return m_barnner.ShowWithAster();
        }

        public string PrintWeak()
        {
            return m_barnner.ShowWithParen();
        }
    }
}
