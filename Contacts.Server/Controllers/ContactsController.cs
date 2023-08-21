using Contacts.Application.Common;
using Contacts.Application.Contacts.Commands.AddContact;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Commands.UpdateContact;
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
        [HttpPost]
        public async Task<ActionResult> AddContact([FromBody] Contact contact)
        {
            var query = new ApiAddContactCommand();
            query.Contact = contact;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateContact([FromBody] Contact contact)
        {
            var query = new ApiUpdateContactCommand();
            query.Contact = contact;
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveContact(Guid id)
        {
            var query = new ApiRemoveContactCommand();
            query.Id = id;
            await Mediator.Send(query);
            return NoContent();
        }
    }
}
