namespace ChatSystem.Api.Controllers
{
    using ChatSystem.Services.Data.Contracts;
    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class AvatarsController : ApiController
    {
        private readonly IAvatarsService avatars;

        public AvatarsController(IAvatarsService avatarServicePassed)
        {
            this.avatars = avatarServicePassed;
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var currentUsername = this.User.Identity.Name;
            var file = this.avatars.Get(currentUsername);
            return this.Ok(file);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post()
        {
            ByteArrayResource res = null;
            var currentUsername = this.User.Identity.Name; // TODO

            try
            {
                Request.Content.ReadAsMultipartAsync<MultipartMemoryStreamProvider>(new MultipartMemoryStreamProvider()).ContinueWith((task) =>
                {
                    MultipartMemoryStreamProvider provider = task.Result;

                    foreach (HttpContent content in provider.Contents)
                    {
                        res = new ByteArrayResource(content.ReadAsByteArrayAsync().Result);

                        //Entry uploadFileEntry = dropbox.UploadFileAsync(
                        //res, "/plane.jpg", true, null, CancellationToken.None).Result;
                    }
                });


                this.avatars.Post(res, currentUsername);
                return this.Ok("Avatar successfully added.");
            }
            catch (Exception)
            {
                return this.BadRequest();
            }
        }

        public IHttpActionResult Delete(string avatarUrl)
        {
            var currentUsername = this.User.Identity.Name;
            avatars.Delete(currentUsername);
            return this.Ok("Avatar successfully added.");
        }
    }
}