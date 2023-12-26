using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wardrobe.Application.Exceptions
{
    public class AuthenticationErrorException : Exception
    {
        public AuthenticationErrorException() : base("Giris Hatali")
        {
        }

        public AuthenticationErrorException(string? message) : base(message)
        {
        }
    }
}
