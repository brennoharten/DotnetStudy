using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioApi.Exceptions
{
    public class MyCustomException : Exception
    {
        public MyCustomException(string message) : base(message)
        {
        }
    }
}