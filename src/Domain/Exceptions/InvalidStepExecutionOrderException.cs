using System;

namespace Domain.Exceptions
{
    public class InvalidStepExecutionOrderException : Exception
    {
        public InvalidStepExecutionOrderException()
            : base("Invalid order of step execution")
        {
        }
    }
}