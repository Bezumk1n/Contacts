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
        private readonly IContactsDbContext _context;
        private readonly IStore<Contact> _store;

        public UpdateContactCommandHandler(IContactsDbContext context, IStore<Contact> store)
        {
            _context = context;
            _store = store;
        }
        public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var entity = _context.Contacts.FirstOrDefault(q => q.Id == request.Contact.Id);

            if (entity.Name != request.Contact.Name) entity.Name = request.Contact.Name;
            if (entity.Surname != request.Contact.Surname) entity.Surname = request.Contact.Surname;
            if (entity.Address != request.Contact.Address) entity.Address = request.Contact.Address;
            if (entity.Email != request.Contact.Email) entity.Email = request.Contact.Email;
            if (entity.Description != request.Contact.Description) entity.Description = request.Contact.Description;
            if (entity.PhoneNumber != request.Contact.PhoneNumber) entity.PhoneNumber = request.Contact.PhoneNumber;
            _context.SaveChanges();

            _store.ReplaceItem(_store.SelectedItem, entity);
        }
    }
}
