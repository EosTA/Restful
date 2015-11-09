using System.Linq;
using ChatSystem.Api.Models.Messages;
using ChatSystem.Data;

namespace ChatSystem.Api.Controllers
{
    using System.Web.Http;

    public class MessagesController : ApiController
    {
        public IHttpActionResult Get()
        {
            var db = new ChatSystemDbContext();
            var result = db
                .ChatMessages
                .OrderBy(m => m.SentOn)
                .Take(10)
                .ToList();
            return this.Ok(result);
        }

        [Route("api/messages/all")]
        public IHttpActionResult Get(int page, int pageSize = 10)
        {
            var db = new ChatSystemDbContext();
            var result = db
                .ChatMessages
                .OrderBy(m => m.SentOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(SaveMessageRequestModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}