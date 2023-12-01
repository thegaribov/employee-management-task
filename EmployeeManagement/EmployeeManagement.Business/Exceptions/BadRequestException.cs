using System;

namespace EmployeeManagement.Business.Exceptions;

public class BadRequestException : ApplicationException
{
    public BadRequestException(string message)
        : base(message)
    {

    }
}
