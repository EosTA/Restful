namespace ChatSystem.Api.Tests
{
    using ChatSystem.Api;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MyTested.WebApi;

    [TestClass]
    public class TestInitializator
    {
        [AssemblyInitialize]
        public static void Init(TestContext context)
        {
            var configuration = new System.Web.Http.HttpConfiguration();
            WebApiConfig.Register(configuration);
            MyWebApi.IsUsing(configuration);
        }
    }
}
