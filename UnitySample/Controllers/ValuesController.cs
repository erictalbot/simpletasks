using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UnitySample.Service;

namespace UnitySample.Controllers
{
    public class ValuesController : ApiController
    {
        private readonly IMessageService _messageService;

        public ValuesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            string bidon = _messageService.GetMessage();
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}