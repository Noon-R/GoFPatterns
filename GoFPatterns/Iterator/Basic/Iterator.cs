using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Iterator.Basic
{
    public interface Iterator
    {
        bool hasNext();
        object next();
    }
}
