namespace ChatSystem.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;

    using Api.Tests.TestObject;

    using ChatSystem.Common.Constants;

    using Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Models.Messages;

    using MyTested.WebApi;

    using Services.Data.Contracts;

    [TestClass]
    public class MessagesControllerTests
    {
        private IMessagesService messageService;
        private MessagesController controller;
        private MessagesController controllerWithPresence;
        private IPresenceService presenceService;

        [TestInitialize]
        public void Init()
        {
            this.messageService = TestObjectFactory.GetMessagesService();
            this.controller = new MessagesController(this.messageService);
            this.presenceService = TestObjectFactory.GetPresenceService();
            this.controllerWithPresence = new MessagesController(this.messageService, this.presenceService);
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
            this.controllerWithPresence.User = new MockedIPrinciple();
            var result = this.controllerWithPresence
                .Get(this.controller.User.Identity.Name) as OkNegotiatedContentResult<List<MessageResponseModel>>;
            var expected = 3;
            Assert.AreEqual(expected, result.Content.Count);
        }

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectMessageIfOk()
        {
            this.controller.User = new MockedIPrinciple();
            var result = this.controller.Delete(5) as OkNegotiatedContentResult<string>;
            var expected = ResponseMessagesInMessageController.MessageDeletedCorrectly;
            Assert.AreEqual(expected, result.Content);
        }

        [TestMethod]
        public void DeleteMessagesShouldReturnCorrectErrorMessageWhenNoUserIsProvided()
        {
            var result = this.controllerWithPresence.Delete(5) as BadRequestErrorMessageResult;
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

            this.controllerWithPresence.User = asker;
            var result = this.controllerWithPresence.Put("Pesho") as BadRequestErrorMessageResult;
            var expected = ErrorsInMessageController.ErrorActionNotTaken;
            Assert.AreEqual(expected, result.Message);
        }
    }
}
