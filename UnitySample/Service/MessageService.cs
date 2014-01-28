using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnitySample.Service
{
    public class MessageService : IMessageService
    {
        private static volatile MessageService instance;
        private static object syncRoot = new Object();

        private IMessageService _messageServiceCopy;

        private MessageService() { }

        public static MessageService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessageService();
                    }
                }
                return instance;
            }
        }

        string IMessageService.GetMessage()
        {
            return this.GetHashCode().ToString();

        }

        void IMessageService.StoreMessageServiceCopy(ref IMessageService messageService)
        {
            _messageServiceCopy = messageService;       // _messageServiceCopy contient une reference vers messageService qui est l'instance envoye depuis de controlleur.
        }

        IMessageService IMessageService.GetMessageServiceCopy()
        {
            return _messageServiceCopy;
        }
    }
}