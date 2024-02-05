using System.Net;

namespace SchoolManagement.Application.Exceptions;

public class NullableException : CustomException
{
    public NullableException(string message)
        : base(message, null, HttpStatusCode.BadRequest)
    {
    }
}