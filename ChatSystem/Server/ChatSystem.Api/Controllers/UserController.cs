namespace ChatSystem.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using ChatSystem.Common.Constants;
    using ChatSystem.Services.Data.Contracts;
    using Models.Users;

    public class UserController : ApiController
    {

        private readonly IUserService users;

        public UserController(IUserService usersServicePassed)
        {
            this.users = usersServicePassed;
        }
        [HttpGet]
        [Route("api/users")]
        [Authorize]
        public IHttpActionResult Get()
        {
            var thatPerson = this.User.Identity.Name;

            var result = this.users
            .All()
            .Select(UserResponseModel.FromModel)
            .ToList();

            return this.Ok(result);
        }

        private void AddError(List<UserResponseModel> result)
        {
            result.Add(new UserResponseModel
            {
                UserName = ErrorsInMessageController.ErrorNoMessages
            });
        }
    }
}
