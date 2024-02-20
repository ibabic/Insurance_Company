using System;

namespace Partners.DataAccess.Utility.ExceptionHandling
{
    public class UniqueKeyViolationException : Exception
    {
        public UniqueKeyViolationException() { }

        public UniqueKeyViolationException(string message) : base(message) { }

        public UniqueKeyViolationException(string message, Exception innerException) : base(message, innerException) { }
    }
}