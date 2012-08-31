using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crow.Library.Foundation.Common.Account;

namespace Crow.Library.Common.Account
{
    internal class UnsafeAccountService : IAccountService
    {
        private UserContext m_CurrentUser;

        public UserContext CurrentUser
        {
            get { return m_CurrentUser; }
            set { m_CurrentUser = value; }
        }
    }
}