using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Exceptions
{
    public class EmailWasNotGivenException : Exception
    {
            public EmailWasNotGivenException() : base("Er was geen email ingevoerd.") { }
            public EmailWasNotGivenException(string message) : base(message) { }
            public EmailWasNotGivenException(string message, Exception inner) : base(message, inner) { }
    }
}
