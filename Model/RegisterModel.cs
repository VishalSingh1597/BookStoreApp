using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class RegisterModel
    {
        public int UsersId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public bool ISAdmin { get; set; }
    }
}