namespace ChatSystem.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using ChatSystem.Api.Models.Messages;
    using ChatSystem.Common.Constants;
    using ChatSystem.Services.Data.Contracts;

    public class MessagesController : ApiController
    {
        private readonly IMessagesService messages;

        public MessagesController(IMessagesService messageServicePassed)
        {
            this.messages = messageServicePassed;
        }

        [HttpGet]
        [Route("api/messages/{username}")]
        [Authorize]
        public IHttpActionResult Get(string username)
        {
            var thatPerson = this.User.Identity.Name;

            var result = new List<MessageResponseModel>();
            try
            {
                result = this.messages
                .All(username, thatPerson)
                .Select(MessageResponseModel.FromModel)
                .ToList();
            }
            catch (NotSupportedException ex)
            {
                result.Add(new MessageResponseModel
                {
                    Message = "Basi mamata"
                });
            }
            return this.Ok(result);
        }

        [HttpGet]
        [Route("api/messages/{username}/{page}/{pageSize}")]
        [Authorize]
        public IHttpActionResult Get(string username, int page, int pageSize)
        {
            var thatPerson = this.User.Identity.Name;
            var result = new List<MessageResponseModel>();
            try
            {
                result = this.messages
                .All(username, thatPerson, page, pageSize)
                .Select(MessageResponseModel.FromModel)
                .ToList();
            }
            catch (NotSupportedException ex)
            {
                result.Add(new MessageResponseModel
                {
                    Message = "Basi mamata"
                });
            }
            return this.Ok(result);
        }

        [Authorize]
        public IHttpActionResult Post(SaveMessageRequestModel model)
        {
            var sender = this.User.Identity.Name;

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.messages.Add(model.Message, sender, model.Receiver);
            return this.Ok();
        }

        [HttpPut]
        [Route("api/messages/{messageId}")]
        [Authorize]
        public IHttpActionResult Put(int messageId, EditMessageRequestModel model)
        {
            if (model == null || !model.IsValid())
            {
                return this.BadRequest();
            }

            var asker = this.User.Identity.Name;
            var message = this.messages.GetMessage(messageId).FirstOrDefault();

            if (message.Sender.UserName != asker)
            {
                return this.BadRequest();
            }

            var result = this.messages.ChangeMessage(messageId, model.IsChangingDate, model.Message);

            if (result)
            {
                return this.Ok();
            }

            return this.BadRequest();
        }

        [HttpPut]
        [Route("api/messages/all/{correspondent}")]
        [Authorize]
        public IHttpActionResult Put(string correspondent)
        {
            var asker = this.User.Identity.Name;

            var result = this.messages.SetReadToAll(asker, correspondent);

            if (result)
            {
                return this.Ok();
            }

            return this.BadRequest();
        }
    }
}