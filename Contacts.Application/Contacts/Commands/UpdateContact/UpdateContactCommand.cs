using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest
    { 
        public Contact Contact { get; set; }
    }
    public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
    {
        private readonly IStore<Contact> _store;
        private readonly ICommonApiClient _client;

        public UpdateContactCommandHandler(IStore<Contact> store, ICommonApiClient client)
        {
            _store = store;
            _client = client;
        }
        public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            //string uri = "http://192.168.0.175:5000/api/Contacts/UpdateContact";
            string uriLocal = "http://localhost:5000/api/Contacts/UpdateContact";
            var contact = await _client.PostData<Contact>(uriLocal, request.Contact);
            _store.ReplaceItem(_store.SelectedItem, contact);

        }
    }
}
