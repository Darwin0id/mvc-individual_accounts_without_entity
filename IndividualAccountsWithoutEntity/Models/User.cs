﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AuthDemo1.Models
{
    public class User : IUser
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            Roles = new List<string>();
        }

        public virtual string Email { get; set; }
        public List<string> Roles { get; set; }
        public virtual string Password { get; set; }
        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        public string Id { get; }
        public string UserName { get; set; }

        public virtual void AddRole(string role)
        {
            Roles.Add(role);
        }

        public virtual void RemoveRole(string role)
        {
            Roles.Remove(role);
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}