using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Crow.Library.Foundation.Common.Messages;

namespace Crow.Library.Common.Messages
{
    public class DebugMessagesService : IMessagingService
    {
        public void ShowErrorMessage(string message)
        {
            Debug.WriteLine(message, "Error");
        }
        public void ShowErrorMessage(string message, params object[] args)
        {
            if (args != null)
            {
                message = string.Format(message, args);
            }
            ShowErrorMessage(message);
        }

        public void ShowInformationMessage(string message)
        {
            Debug.WriteLine(message, "Information");
        }
        public void ShowInformationMessage(string message, params object[] args)
        {
            if (args != null)
            {
                message = string.Format(message, args);
            }
            ShowInformationMessage(message);
        }

        public DialogResults AskQuestion(string question)
        {
            return DialogResults.OK;
        }

        public DialogResults AskQuestion(string question, params object[] args)
        {
            return DialogResults.OK;
        }
    }
}