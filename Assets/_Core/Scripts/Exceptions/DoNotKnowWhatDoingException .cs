using System;

namespace LearnGame.Exceptions
{
    public class DoNotKnowWhatDoingException: Exception
    {
        public const string BaseMessage = "Didn't find the target and went off into the distance.";
        public DoNotKnowWhatDoingException() : base(BaseMessage) { }
        public DoNotKnowWhatDoingException(string message) : base(message) { }
        public DoNotKnowWhatDoingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
