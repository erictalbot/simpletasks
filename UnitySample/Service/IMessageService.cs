using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitySample.Service
{
    public interface IMessageService
    {
        string GetMessage();
        void StoreMessageServiceCopy(ref IMessageService messageService);
        IMessageService GetMessageServiceCopy();
    }
}