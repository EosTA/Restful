namespace ChatSystem.Api.Controllers
{
    using ChatSystem.Services.Data.Contracts;
    using Spring.IO;
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
            var avatarUrl = this.avatars.Get(currentUsername);
            return this.Ok(avatarUrl);
        }

        [EnableCors("*", "*", "*")]
        public IHttpActionResult Post()
        {
            var currentUsername = this.User.Identity.Name;

            try
            {
                var contents = this.Request.Content.ReadAsMultipartAsync().Result;
                HttpContent content = contents.Contents[0];
                ByteArrayResource resource = new ByteArrayResource(content.ReadAsByteArrayAsync().Result);
                this.avatars.Post(resource, currentUsername);
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