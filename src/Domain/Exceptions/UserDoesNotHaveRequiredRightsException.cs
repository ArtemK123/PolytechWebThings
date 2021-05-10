using System;

namespace Domain.Exceptions
{
    public class UserDoesNotHaveRequiredRightsException : Exception
    {
        public UserDoesNotHaveRequiredRightsException(string actionDescription)
            : base($"User does not have rights to perform this action - {actionDescription}")
        {
        }
    }
}