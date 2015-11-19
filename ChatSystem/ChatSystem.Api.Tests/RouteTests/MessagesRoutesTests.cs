namespace ChatSystem.Api.Tests.RouteTests
{
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using System.Web.Http;

    [TestClass]
    public class MessagesRoutesTests
    {
        [TestInitialize]
        public void Init()
        {
            var httpConfig = new HttpConfiguration();
        }


        [TestMethod]
        public void GetMessagesUsernameShouldHaveHttpMethodGet()
        {
            MyWebApi.Routes()
                .ShouldMap("api/messages/User1")
                .WithHttpMethod("GET").WithRequestHeader("ContentType","application/json");
        }

        [TestMethod]
        public void GetPaginMessagesByExistingUserShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/messages/User3/1/1")
                .To<MessagesController>(m => m.Get("User1"));
        }

        [TestMethod]
        public void GetPaginMessagesByExistingUserShouldReturnOk()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/messages/User1/1/1")
                .To<MessagesController>(m => m.Get("User1")).AndAlso().ToValidModelState();
        }

        [TestMethod]
        [ExpectedException(typeof(MyTested.WebApi.Exceptions.RouteAssertionException))]
        public void GetPaginMessagesByNonExistingUserShouldMapInCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/messages/User1/1/1")
                .To<MessagesController>(m => m.Get("User11")).ToInvalidModelState();
        }
    }
}
