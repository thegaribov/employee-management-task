using System;

namespace EmployeeManagement.Business.Exceptions;

public class UnauthorizedException : ApplicationException
{
    public UnauthorizedException(string message)
        : base(message)
    {

    }
}
