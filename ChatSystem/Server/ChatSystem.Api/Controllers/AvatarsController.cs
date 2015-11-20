namespace ChatSystem.Api.Controllers
{
    using System;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Services.Data.Contracts;

    using Spring.IO;

    [Authorize]
    [EnableCors("*", "*", "*")]
    public class AvatarsController : ApiController
    {
        private readonly IAvatarsService avatars;

        public AvatarsController(IAvatarsService avatarServicePassed)
        {
            this.avatars = avatarServicePassed;
        }

        // /api/avatars => returns url as string
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var currentUsername = this.User.Identity.Name;
            var avatarUrl = this.avatars.Get(currentUsername);
            return this.Ok(avatarUrl);
        }

        // /api/avatars
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
            this.avatars.Delete(currentUsername);
            return this.Ok("Avatar successfully added.");
        }
    }
}