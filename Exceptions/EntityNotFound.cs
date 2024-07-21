namespace Stackoverflow_Light.Exceptions;

public class EntityNotFound : Exception
{
    public EntityNotFound (string message) : base(message){}
}