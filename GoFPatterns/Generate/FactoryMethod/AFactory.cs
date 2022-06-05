using System;
using System.Collections.Generic;
using System.Text;

namespace GoFPatterns.Generate.FactoryMethod
{
    public abstract class AFactory
    {
        public IProduct Create(string key) {
            return CreateProduct(key);
        }

        protected abstract IProduct CreateProduct(string key);

    }
}
