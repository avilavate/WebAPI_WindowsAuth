using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Windows_Auth.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        IEnumerable<Contact> contacts = new List<Contact>
       {
            new Contact { Id = 1, Name = "Steve", Email = "steve@gmail.com", Mobile = "+1(234)35434" },
            new Contact { Id = 2, Name = "Matt", Email = "matt@gmail.com", Mobile = "+1(234)5654" },
            new Contact { Id = 3, Name = "Mark", Email = "mark@gmail.com", Mobile = "+1(234)56789" }
       };

        [Authorize]
        // GET: api/Contacts
        public IEnumerable<Contact> Get()
        {
            return contacts;
        }
    }
}
