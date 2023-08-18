using Contacts.Application.Contacts.Queries.GetContacts;
using Contacts.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Server.Controllers
{
    public class ContactsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<ContactListVM>> GetAllContacts()
        {
            var query = new ApiGetContactsQuerry();
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
