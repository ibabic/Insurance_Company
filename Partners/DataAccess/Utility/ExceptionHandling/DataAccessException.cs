using System;

namespace Partners.DataAccess.Utility.ExceptionHandling
{
    public class DataAccessException : Exception
    {
        public DataAccessException() : base()
        {
        }

        public DataAccessException(string message, string innerMessage) : base(message)
        {
        }

        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}