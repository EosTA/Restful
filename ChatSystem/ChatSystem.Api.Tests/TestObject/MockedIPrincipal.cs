namespace ChatSystem.Api.Tests.TestObject
{
    using System.Security.Principal;

    public class MockedIPrinciple : IPrincipal
    {
        public IIdentity Identity
        {
            get
            {
                return new MockedIIdentity();
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }
    }
}
