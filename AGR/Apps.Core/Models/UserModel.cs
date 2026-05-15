using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGR.Apps.Core.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string RoleCode { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}