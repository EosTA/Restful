namespace ChatSystem.Api.Models.Users
{
    using System;
    using System.Linq.Expressions;
    using ChatSystem.Models;

    public class UserResponseModel
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public static Expression<Func<User, UserResponseModel>> FromModel
        {
            get
            {
                return user => new UserResponseModel
                {
                    UserName = user.UserName
                };
            }
        }
    }
}
