
namespace ChatSystem.Api.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ChatSystem.Api;
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
