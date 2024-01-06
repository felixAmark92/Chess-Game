using System;

namespace MonoGameGameLibrary1;

public class MissingComponentException : Exception
{
    public override string Message { get; }

    public MissingComponentException(string message)
    {
        Message = message;
    }
    public MissingComponentException()
    {
        Message = "The component you tried to add is dependent on another component";
    }
    
}