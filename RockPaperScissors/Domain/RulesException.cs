using System;
using System.Runtime.Serialization;

namespace RockPaperScissors.Domain
{
    [Serializable]
    public class RulesException : Exception
    {
        public RulesException()
        {
        }

        public RulesException(string message)
            : base(message)
        {
        }

        public RulesException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected RulesException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}