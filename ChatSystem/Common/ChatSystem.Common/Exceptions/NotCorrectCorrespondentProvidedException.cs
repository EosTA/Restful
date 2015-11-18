namespace ChatSystem.Common.Exceptions
{
    using System;

    public class NotCorrectCorrespondentProvidedException : Exception
    {
        public NotCorrectCorrespondentProvidedException() : base()
        {
        }

        public NotCorrectCorrespondentProvidedException(string message) : base(message)
        {
        }
    }
}
