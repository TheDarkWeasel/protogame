using UnityEngine;
using System;

public class IllegalStateException : UnityException
{
    public IllegalStateException() : base()
    {

    }
    public IllegalStateException(string message) : base(message)
    {

    }

    public IllegalStateException(string message, Exception innerException) : base(message, innerException)
    {

    }
}
