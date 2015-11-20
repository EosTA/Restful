namespace ChatSystem.Api.Tests.ControllerTests
{
    using System.Linq;
    using System.Web.Http.Results;

    using Controllers;

    using Data.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;

    using Services.Data.Contracts;

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
