using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalog.Exceptions
{
    public class InvalidParameterException : ArgumentException
    {
        public InvalidParameterException(string paramName, string message)
            : base(message, paramName)
        {
        }
    }
}
