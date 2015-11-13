using ChatSystem.Services.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ChatSystem.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/avatar")]
    public class AvatarsController : ApiController
    {
        private readonly IAvatarsService avatars;

        public AvatarsController(IAvatarsService messageServicePassed)
        {
            this.avatars = messageServicePassed;
        }

        public IHttpActionResult Post(string avatarUrl)
        {
            var currentUsername = this.User.Identity.Name;
            avatars.Post(avatarUrl, currentUsername);
            return this.Ok("Avatar successfully added.");
        }

        public IHttpActionResult Delete(string avatarUrl)
        {
            var currentUsername = this.User.Identity.Name;
            avatars.Delete(currentUsername);
            return this.Ok("Avatar successfully added.");
        }
    }
}