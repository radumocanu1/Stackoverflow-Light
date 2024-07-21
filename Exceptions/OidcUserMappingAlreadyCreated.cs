namespace Stackoverflow_Light.Exceptions;

public class OidcUserMappingAlreadyCreated : Exception
{
    public OidcUserMappingAlreadyCreated(string? message) : base(message)
    {
    }
}