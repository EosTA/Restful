
namespace ChatSystem.Api.Tests.ControllerTests
{
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;
    using ChatSystem.Api.Tests.TestObject;
    using System.Net;
    using System.Net.Http;

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

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectMessageIfOk()
        {
            var messageService = TestObjectFactory.GetMessagesService();

            var controller = new MessagesController(messageService);
            controller.User = new MockedIPrinciple();
            var result = controller.Delete(5) as OkNegotiatedContentResult<string>;
            var expected = "Your message has been deleted";
            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectErrorMessageWhenNoUserIsProvided()
        {
            var messageService = TestObjectFactory.GetMessagesService();

            var controller = new MessagesController(messageService);
            var result = controller.Delete(5) as BadRequestErrorMessageResult;
            var expected = "There is some problem with your request";
            Assert.AreEqual(expected, result.Message);

        }
    }
}
