namespace ChatSystem.Api.Providers
{
    using ChatSystem.Common.Constants;
    using IronSharp.IronMQ;

    public class Notificator
    {
        // IronMqRestClient
        private static IronMqRestClient client;

        private Notificator()
        {
            client = Client.New(GlobalConstants.NotificationProductId, GlobalConstants.NotificationToken);
        }

        public static IronMqRestClient GetNotificator()
        {
            if (client == null)
            {
                new Notificator();
            }

            return client;
        }
    }
}