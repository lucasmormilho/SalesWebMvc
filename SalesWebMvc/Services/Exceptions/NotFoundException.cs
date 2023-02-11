using System;

namespace SalesWebMvc.Services.Exceptions
{
    //excessão personalizada
    public class NotFoundException : ApplicationException
    {
        public NotFoundException (string message) : base(message)
        {

        }
    }
}
