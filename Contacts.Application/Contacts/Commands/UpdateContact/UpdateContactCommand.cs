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
            //var entity = _context.Contacts.FirstOrDefault(q => q.Id == request.Contact.Id);

            //if (entity.Name != request.Contact.Name) entity.Name = request.Contact.Name;
            //if (entity.Surname != request.Contact.Surname) entity.Surname = request.Contact.Surname;
            //if (entity.Address != request.Contact.Address) entity.Address = request.Contact.Address;
            //if (entity.Email != request.Contact.Email) entity.Email = request.Contact.Email;
            //if (entity.Description != request.Contact.Description) entity.Description = request.Contact.Description;
            //if (entity.PhoneNumber != request.Contact.PhoneNumber) entity.PhoneNumber = request.Contact.PhoneNumber;
            //_context.SaveChanges();

            //_store.ReplaceItem(_store.SelectedItem, entity);
        }
    }
}
