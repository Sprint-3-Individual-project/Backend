using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException() : base("Er is geen geldige prijs ingevoerd.") { }
        public InvalidPriceException(string message) : base(message) { }
        public InvalidPriceException(string message, Exception inner) : base(message, inner) { }
    }
}
