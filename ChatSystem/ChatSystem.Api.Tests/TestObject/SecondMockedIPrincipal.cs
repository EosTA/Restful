namespace ChatSystem.Api.Tests.TestObject
{
    using System.Security.Principal;

    public class SecondMockedIPrinciple : IPrincipal
    {
        public IIdentity Identity
        {
            get
            {
                return new SecondMockedIIdentity();
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
