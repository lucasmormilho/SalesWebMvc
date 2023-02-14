using System;

namespace SalesWebMvc.Services.Exceptions
{
    public class IntegrityException : ApplicationException
    {
        //tratamento de erro para delete 
        public IntegrityException(string message) : base(message)
        {

        }
    }
}
