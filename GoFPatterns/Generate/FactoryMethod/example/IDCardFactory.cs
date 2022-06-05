using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Generate.FactoryMethod.example
{
    public class IDCardFactory : AFactory
    {
        protected override IProduct CreateProduct(string key)
        {
            return new IDCard(key);
        }
    }
}
