using System;

namespace EmployeeManagement.Business.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException(string message)
        : base(message)
    {

    }
}
