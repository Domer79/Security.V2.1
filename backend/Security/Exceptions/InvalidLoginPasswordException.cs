using System;

namespace Security.Exceptions
{
    public class InvalidLoginPasswordException : Exception
    {
        public InvalidLoginPasswordException(string loginOrEmail, string password)
            :base($"{loginOrEmail} and his password not valid")
        {
            
        }
    }
}