﻿namespace ChatSystem.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using ChatSystem.Common.Constants;
    using ChatSystem.Common.Exceptions;

    using IronSharp.IronMQ;

    using Models.Messages;

    using Providers;

    using Services.Data.Contracts;

    public class MessagesController : ApiController
    {
        private readonly IMessagesService messages;
        private readonly IPresenceService presences;

        public MessagesController(IMessagesService messageServicePassed, IPresenceService presenceServicePassed) : this(messageServicePassed)
        {
            this.presences = presenceServicePassed;
        }

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
                .Select(MessageResponseModel.FromModel(thatPerson))
                .ToList();
            }
            catch (NotSupportedException)
            {
                this.AddError(result);
            }
            catch (NotCorrectCorrespondentProvidedException)
            {
                this.AddError(result);
            }

            this.presences.UpdatePresence(thatPerson);

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
                .Select(MessageResponseModel.FromModel(thatPerson))
                .ToList();
            }
            catch (NotSupportedException)
            {
                this.AddError(result);
            }
            catch (NotCorrectCorrespondentProvidedException)
            {
                this.AddError(result);
            }

            this.presences.UpdatePresence(thatPerson);

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

            if (model == null)
            {
                return this.BadRequest();
            }

            this.messages.Add(model.Message, sender, model.Receiver);

            if (GlobalConstants.IsNotificationEnabled && !this.presences.CheckPresence(model.Receiver) && this.presences != null)
            {
                var notificator = Notificator.GetNotificator();
                QueueClient queue = notificator.Queue(GlobalConstants.NotificationChanel);
                queue.Post(sender);
            }

            this.presences.UpdatePresence(sender);

            return this.Ok(ResponseMessagesInMessageController.MessageInsertedCorrectly);
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

            var result = this.messages.ChangeMessage(messageId, model.IsChangingDate, model.Message, asker);

            if (result)
            {
                return this.Ok(ResponseMessagesInMessageController.MessageEditedCorrectly);
            }

            this.presences.UpdatePresence(asker);

            return this.BadRequest(ErrorsInMessageController.ErrorActionNotTaken);
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
                return this.Ok(ResponseMessagesInMessageController.MessagesUpdatedDateCorrectly);
            }

            this.presences.UpdatePresence(asker);

            return this.BadRequest(ErrorsInMessageController.ErrorActionNotTaken);
        }

        [HttpDelete]
        [Route("api/messages/{messageId}")]
        [Authorize]
        public IHttpActionResult Delete(int messageId)
        {
            var asker = this.User.Identity.Name;

            var result = this.messages.DeleteMessage(messageId, asker);

            if (result)
            {
                return this.Ok(ResponseMessagesInMessageController.MessageDeletedCorrectly);
            }

            this.presences.UpdatePresence(asker);

            return this.BadRequest(ErrorsInMessageController.ErrorActionNotTaken);
        }

        private void AddError(List<MessageResponseModel> result)
        {
            result.Add(new MessageResponseModel
            {
                Message = ErrorsInMessageController.ErrorNoMessages
            });
        }
    }
}