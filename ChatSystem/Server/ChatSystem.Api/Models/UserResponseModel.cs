namespace ChatSystem.Api.Models.Users
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    public class UserResponseModel
    {
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

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
