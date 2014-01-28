using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnitySample.Service;

namespace UnitySample.Controllers
{
    public class HomeController : Controller
    {
        private IMessageService _messageService;

        public HomeController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public ActionResult Index()
        {
            string bidon = _messageService.GetMessage();            
            
            // Comparer la reference, la premiere fois sera faux mais vrai les autres fois.
            bool memeReference = object.ReferenceEquals(_messageService, _messageService.GetMessageServiceCopy());
            if (!memeReference)             
                _messageService.StoreMessageServiceCopy(ref _messageService);

            string versionOfMVC = typeof(Controller).Assembly.GetName().Version.ToString();

            return View();
        }
    }
}
