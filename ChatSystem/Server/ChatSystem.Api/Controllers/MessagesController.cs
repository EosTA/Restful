namespace ChatSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using ChatSystem.Api.Models.Messages;
    using ChatSystem.Data;
    using ChatSystem.Services.Data;
    using ChatSystem.Services.Data.Contracts;

    public class MessagesController : ApiController
    {
        private readonly IMessagesService messages;

        public MessagesController(IMessagesService messageServicePassed)
        {
            this.messages = messageServicePassed;
        }

        public IHttpActionResult Get()
        {
            var result = this.messages
                .All()
                .Select(MessageResponseModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        [Route("api/messages/all")]
        public IHttpActionResult Get(int page, int pageSize)
        {
            var result = this.messages
                .All(page,pageSize)
                .Select(MessageResponseModel.FromModel)
                .ToList();

            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(SaveMessageRequestModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.messages.Add(model.Message, model.Sender, model.Receiver);
            return this.Ok();
        }
    }
}