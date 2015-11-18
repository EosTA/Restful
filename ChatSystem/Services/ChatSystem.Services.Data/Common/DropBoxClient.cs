namespace ChatSystem.Api.Common
{
    using ChatSystem.Common.Constants;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.Social.OAuth1;
    using System;
    using System.Diagnostics;
    using System.IO;

    class DropBoxClient
    {
        // Register your own Dropbox app at https://www.dropbox.com/developers/apps
        // with "Full Dropbox" access level and set your app keys and app secret below
        private string dropboxAppKey;
        private string dropboxAppSecret;
        private const string OAuthTokenFileName = "OAuthTokenFileName.txt";

        private DropboxServiceProvider dropboxServiceProvider;

        public DropBoxClient(string appKey, string appSecret)
        {
            this.dropboxAppKey = appKey;
            this.dropboxAppSecret = appSecret;
            this.dropboxServiceProvider
                = new DropboxServiceProvider(dropboxAppKey, dropboxAppSecret, AccessLevel.AppFolder);
        }

        public string Upload(IResource resource, string storeName)
        {
            // Authenticate the application (if not authenticated) and load the OAuth token
            //if (!File.Exists(OAuthTokenFileName))
            //{
            //    AuthorizeAppOAuth(dropboxServiceProvider);
            //}

            //OAuthToken oauthAccessToken = LoadOAuthToken();

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider
                .GetApi(AuthorizationConstants.DropBoxOAuthAccessTokenValue, AuthorizationConstants.DropBoxOAuthAccessTokenSecret);

            // Display user name (from his profile)
            DropboxProfile profile = dropbox.GetUserProfileAsync().Result;
            Console.WriteLine("Hi " + profile.DisplayName + "!");

            // Upload a file
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                resource, storeName).Result;
            Console.WriteLine("Uploaded a file: {0}", uploadFileEntry.Path);

            // Share a file
            //DropboxLink sharedUrl = dropbox.GetShareableLinkAsync(uploadFileEntry.Path).Result;
            //Process.Start(sharedUrl.Url);
            return uploadFileEntry.Path;
        }

        public byte[] GetFile(string fileUrl)
        {
            //if (!File.Exists(OAuthTokenFileName))
            //{
            //    AuthorizeAppOAuth(dropboxServiceProvider);
            //}

            //OAuthToken oauthAccessToken = LoadOAuthToken();

            var serviceProvider = dropboxServiceProvider
                .GetApi(AuthorizationConstants.DropBoxOAuthAccessTokenValue, AuthorizationConstants.DropBoxOAuthAccessTokenSecret);
            var file = serviceProvider.DownloadFileAsync(fileUrl).Result;
            return file.Content;
        }

        private OAuthToken LoadOAuthToken()
        {
            string[] lines = File.ReadAllLines(OAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);
            return oauthAccessToken;
        }

        private void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            // Authorization without callback url
            Console.Write("Getting request token...");
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            Console.WriteLine("Done.");

            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(
                oauthToken.Value, parameters);
            Console.WriteLine("Redirect the user for authorization to {0}", authenticateUrl);
            Process.Start(authenticateUrl);
            Console.Write("Press [Enter] when authorization attempt has succeeded.");
            Console.ReadLine();

            Console.Write("Getting access token...");
            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken =
                dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            Console.WriteLine("Done.");

            string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
            File.WriteAllLines(OAuthTokenFileName, oauthData);
        }
    }


}