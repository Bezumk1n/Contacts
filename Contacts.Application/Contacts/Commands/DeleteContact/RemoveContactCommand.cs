using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class RemoveContactCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class RemoveContactCommandHandler : IRequestHandler<RemoveContactCommand>
    {
        private readonly IStore<Contact> _contactsStore;
        private readonly ICommonApiClient _client;

        public RemoveContactCommandHandler(IStore<Contact> contactsStore, ICommonApiClient client)
        {
            _contactsStore = contactsStore;
            _client = client;
        }
        public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //string uri = "http://192.168.0.175:5000/api/Contacts/RemoveContact";
                string uriLocal = "http://localhost:5000/api/Contacts/RemoveContact/";
                await _client.RemoveData(uriLocal + request.Id);
                _contactsStore.RemoveItem(request.Id);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
