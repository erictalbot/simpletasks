using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Services
{
    public class MessageService: IMessageService
    {
        public string GetMessage()
        {
            return "Hi there";
        }
    }
}