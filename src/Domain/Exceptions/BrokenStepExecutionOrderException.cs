using System;

namespace Domain.Exceptions
{
    public class BrokenStepExecutionOrderException : Exception
    {
        public BrokenStepExecutionOrderException(string message)
            : base(message)
        {
        }
    }
}