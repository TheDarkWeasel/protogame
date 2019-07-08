using UnityEngine;
using System;

public class NotYetImplementedException : UnityException
{
    public NotYetImplementedException() : base()
    {

    }
    public NotYetImplementedException(string message) : base(message)
    {

    }

    public NotYetImplementedException(string message, Exception innerException) : base(message, innerException)
    {

    }
}
