using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.Foundation.Common.Messages
{
    public interface IMessagingService
    {
        void ShowErrorMessage(string message);
        void ShowErrorMessage(string message, params object[] args);

        void ShowInformationMessage(string message);
        void ShowInformationMessage(string message, params object[] args);

        DialogResults AskQuestion(string question);
        DialogResults AskQuestion(string question, params object[] args);
    }
}