using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthDemo1.Models.Auth
{
    public class DbContext : IDisposable
    {
        private DbContext(IList<User> users)
        {
            Users = users;
        }

        public IList<User> Users { get; set; }
        public void Dispose()
        {
           
        }

        public static DbContext Create()
        {
            var users = new List<User>
            {
                new User
                {
                    UserName = "matija@mail.com",
                    Email = "matija@mail.com",
                    Password = "test",
                    Roles = new List<string> {"Admin"}
                },
                new User
                {
                    UserName = "demo@mail.com",
                    Email = "demo@mail.com",
                    Password = "test",
                    Roles = new List<string> {"User"}
                }
            };

            return new DbContext(users);
        }
    }
}