namespace Stackoverflow_Light.Exceptions;

public class QuestionNotFound : Exception
{
    public QuestionNotFound (string message) : base(message){}
}