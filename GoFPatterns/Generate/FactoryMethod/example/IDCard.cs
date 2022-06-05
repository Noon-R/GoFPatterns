using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Generate.FactoryMethod.example
{
    class IDCard : IProduct
    {
        string _Owner;
        public IDCard(string owner) {
            _Owner = owner;
        }
        public string use()
        {
            return _Owner;
        }
    }
}
