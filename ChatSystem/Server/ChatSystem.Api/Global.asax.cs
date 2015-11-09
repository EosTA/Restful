namespace ChatSystem.Api
{
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            DatabaseConfig.Initialize();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
