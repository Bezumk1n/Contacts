using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Contacts.Application.Contacts.Commands.AddContact
{
    public class AddContactCommand : IRequest
    {
        public Contact Contact;
    }
    public class AddContactCommandHandler : IRequestHandler<AddContactCommand>
    {
        private readonly IStore<Contact> _contactsStore;
        private readonly IContactsApiService _contactsApiService;

        public AddContactCommandHandler(IStore<Contact> contactsStore, IContactsApiService contactsApiService)
        {
            _contactsStore = contactsStore;
            _contactsApiService = contactsApiService;
        }
        public async Task Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _contactsApiService.AddContact(request.Contact);
                _contactsStore.AddItem(contact);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
