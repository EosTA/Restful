namespace ChatSystem.Api.Tests.TestObject
{
    using System;
    using System.Security.Principal;

    public class SecondMockedIIdentity : IIdentity
    {
        public string AuthenticationType
        {
            get
            {
                return string.Empty;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "User1";
            }
        }
    }
}
