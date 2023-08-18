using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Contacts.Queries.GetContacts
{
    public class GetContactsQuerry : IRequest
    {
    }
    public class GetContactsQuerryHandler : IRequestHandler<GetContactsQuerry>
    {
        private readonly IStore<Contact> _contactsStore;
        private readonly ICommonApiClient _client;

        public GetContactsQuerryHandler(IStore<Contact> contactsStore, ICommonApiClient client)
        {
            _contactsStore = contactsStore;
            _client = client;
        }
        public async Task Handle(GetContactsQuerry request, CancellationToken cancellationToken)
        {
            //string uri = "http://192.168.0.175:5000/api/Contacts/AddContact";
            string uriLocal = "http://localhost:5000/api/Contacts/GetAllContacts";
            var result = await _client.GetData<ContactListVM>(uriLocal);
            _contactsStore.AddItems(result.Contacts);
        }
    }
}
