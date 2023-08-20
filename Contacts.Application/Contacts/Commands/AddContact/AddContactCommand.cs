using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Contacts.Application.Contacts.Commands.AddContact
{
    public class AddContactCommand : IRequest
    {
        public Contact Contact { get; set; }
    }
    public class AddContactCommandHandler : IRequestHandler<AddContactCommand>
    {
        private readonly IStore<Contact> _contactsStore;
        private readonly ICommonApiClient _client;

        public AddContactCommandHandler(IStore<Contact> contactsStore, ICommonApiClient client)
        {
            _contactsStore = contactsStore;
            _client = client;
        }
        public async Task Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //string uri = "http://192.168.0.175:5000/api/Contacts/AddContact";
                string uriLocal = "http://localhost:5000/api/Contacts/AddContact";
                var contact = await _client.PostData<Contact>(uriLocal, request.Contact);
                _contactsStore.AddItem(contact);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
