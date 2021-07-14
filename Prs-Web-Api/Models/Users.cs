using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Prs_Web_Api.Models
{
    public partial class Users
    {
        public Users()
        {
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Reviewer { get; set; }
        public bool Admin { get; set; }

        public virtual ICollection<Request> Request { get; set; }
    }
}
