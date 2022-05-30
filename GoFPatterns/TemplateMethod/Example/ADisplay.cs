using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.TemplateMethod.Example
{
    public abstract class ADisplay
    {
        protected abstract string Open();
        protected abstract string Print();
        protected abstract string Close();

        public string Display() {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Open());
            for (int i = 0; i < 5; i++) {
                stringBuilder.Append(Print());
            }
            stringBuilder.AppendLine(Close());

            return stringBuilder.ToString();
        }

    }
}
