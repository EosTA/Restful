
namespace ChatSystem.Api.Tests.ControllerTests
{
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using System.Web.Http;

    [TestClass]
    public class MessagesControllerTests
    {
        [TestMethod]
        public void GetMethodShouldHaveAuthorizeAttribute()
        {
            var messageService = TestObjectFactory.GetMessagesService();

            MyWebApi
                .Controller<MessagesController>()
                .WithResolvedDependencyFor(messageService)
                .Calling(m => m.Get("User1"))
                .ShouldHave()
                .ActionAttributes(attr => attr.ContainingAttributeOfType<AuthorizeAttribute>());
        }

        [TestMethod]
        public void CallingGetMethodWithValidUserNameShouldReturnStatusCodeOk()
        {
            var messageService = TestObjectFactory.GetMessagesService();

            MyWebApi
                .Controller<MessagesController>()
                .WithResolvedDependencyFor(messageService)
                .Calling(m => m.Get("User1"))
                .ShouldReturn()
                .Ok();
        }
    }
}
