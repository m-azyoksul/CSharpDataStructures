using System;

namespace Qc.Result
{
    public class ErrorNotAccountedForException : Exception
    {
        public ErrorNotAccountedForException() : base("This error type is not accounted for.")
        {
        }
    }
}
