using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common.Account
{
    public class UserContext
    {
        public string Username { get; set; }
        public DateTime LoginDate { get; set; }

        public int UserId { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }
    }
}