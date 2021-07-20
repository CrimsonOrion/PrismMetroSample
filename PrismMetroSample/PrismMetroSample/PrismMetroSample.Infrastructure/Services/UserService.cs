using System.Collections.Generic;

using PrismMetroSample.Infrastructure.Models;

namespace PrismMetroSample.Infrastructure.Services
{
    public class UserService : IUserService
    {
        public List<User> GetAllUsers()
        {
            List<User> allUsers = new List<User>()
           {
               new User(){Id=1,LoginId="Admin",Password="Admin123"},
               new User(){Id=1,LoginId="Ryzen",Password="123456"},
               new User(){Id=1,LoginId="Test",Password="Test123"},
           };
            return allUsers;
        }
    }
}
