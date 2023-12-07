using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct.Exceptions
{
    public class EmptyProductException : Exception
    {
        public EmptyProductException() : base("Er missen wat properties bij het product.") { }
        public EmptyProductException(string message) : base(message) { }
        public EmptyProductException(string message, Exception inner) : base(message, inner) { }
    }
}
