namespace ChatSystem.Api.Tests.ControllerTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using Controllers;
    using Services.Data.Contracts;
    using System.Linq;
    using ChatSystem.Models;
    using System.Web.Http.Results;

    [TestClass]
    public class AvatarsControllerTests
    {
        private IQueryable<User> users;
        private IAvatarsService avatarsService;
        private AvatarsController avatarController;

        [TestInitialize]
        public void Init()
        {
            this.users = TestObjectFactory.Users;
            this.avatarsService = TestObjectFactory.GetAvatarsService();
            this.avatarController = new AvatarsController(this.avatarsService);
        }

        [TestMethod]
        public void AvatarGetMethodShouldReturnStatusCodeOk()
        {
            MyWebApi
                .Controller<AvatarsController>()
                .WithResolvedDependencyFor(this.avatarsService)
                .Calling(a => a.Get())
                .ShouldReturn()
                .Ok();
        }

        [TestMethod]
        public void AvatarGetMethodShouldReturnCorrectValue()
        {
            var actual = this.users.FirstOrDefault().AvatarUrl;
            var expected = this.avatarController.Get() as OkNegotiatedContentResult<string>;
            Assert.AreEqual(actual, expected.Content);
        }
    }
}
