using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Adapter.example
{
    class Barnner
    {
        private readonly string m_text;
        public Barnner(string str) {
            m_text = str;
        }

        public string ShowWithParen() {
            return "(" + m_text + ")";
        }
        public string ShowWithAster() {
            return "*" + m_text + "*";

        }
    }
}
