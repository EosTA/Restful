namespace ChatSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;

    [TestClass]
    public class AvatarRouteTests
    {
        [TestMethod]
        public void AvatarsGetMethodShouldMapRouteCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/avatars")
                .WithHttpMethod("GET").ToValidModelState();
        }

        [TestMethod]
        public void ApiPostMethodShouldMapRouteCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/avatars")
                .WithHttpMethod("POST").ToValidModelState();
        }

        [TestMethod]
        public void ApiDeleteShouldReturnNotAllowedMethod()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/avatars")
                .WithHttpMethod("DELETE").ToNotAllowedMethod();
        }
    }
}
