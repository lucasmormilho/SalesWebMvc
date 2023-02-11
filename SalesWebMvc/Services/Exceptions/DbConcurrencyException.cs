using System;

namespace SalesWebMvc.Services.Exceptions
{
    //excessão personalizada
    public class DbConcurrencyException : ApplicationException
    {
        public DbConcurrencyException(string message): base(message)
        {

        }
    }
}
