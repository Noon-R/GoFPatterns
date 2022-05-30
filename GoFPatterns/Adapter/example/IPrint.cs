using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Adapter.example
{
    public interface IPrint
    {

        string PrintWeak();
        string PrintStrong();
    }
}
