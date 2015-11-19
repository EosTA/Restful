
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
    using Services.Data.Contracts;
    using ChatSystem.Common.Constants;

    [TestClass]
    public class MessagesControllerTests
    {
        private IMessagesService messageService;
        private MessagesController controller;

        [TestInitialize]
        public void Init()
        {
            this.messageService = TestObjectFactory.GetMessagesService();
            this.controller = new MessagesController(this.messageService);
        }

        [TestMethod]
        public void GetMethodShouldHaveAuthorizeAttribute()
        {

            MyWebApi
                .Controller<MessagesController>()
                .WithResolvedDependencyFor(this.messageService)
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
                .WithResolvedDependencyFor(this.messageService)
                .Calling(m => m.Get("User1"))
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectMessageIfOk()
        {
            this.controller.User = new MockedIPrinciple();
            var result = controller.Delete(5) as OkNegotiatedContentResult<string>;
            var expected = ResponseMessagesInMessageController.MessageDeletedCorrectly;
            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectErrorMessageWhenNoUserIsProvided()
        {
            var result = this.controller.Delete(5) as BadRequestErrorMessageResult;
            var expected = ErrorsInMessageController.ErrorActionNotTaken;
            Assert.AreEqual(expected, result.Message);

        }

        [TestMethod]
        public void PutMessageWithCorrectCorrespondantAndUserShouldReturnCorrectMessage()
        {
            var asker = new MockedIPrinciple();
            var correspondant = new SecondMockedIPrinciple();

            this.controller.User = asker;
            var result = this.controller.Put(correspondant.Identity.Name) as OkNegotiatedContentResult<string>;
            var expected = ResponseMessagesInMessageController.MessagesUpdatedDateCorrectly;
            Assert.AreEqual(expected, result.Content);

        }

        [TestMethod]
        public void PutMessageWithIncorrectCorrespondantShouldReturnCorrectMessage()
        {
            var asker = new MockedIPrinciple();

            this.controller.User = asker;
            var result = this.controller.Put("Pesho") as BadRequestErrorMessageResult;
            var expected = ErrorsInMessageController.ErrorActionNotTaken;
            Assert.AreEqual(expected, result.Message);

        }
    }
}
