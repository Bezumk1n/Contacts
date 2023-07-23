using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Contacts.Commands.AddContact
{
    public class AddContactCommand : IRequest
    {
        public Contact Contact;
    }
    public class AddContactCommandHandler : IRequestHandler<AddContactCommand>
    {
        private readonly IContactsDbContext _context;
        private readonly IStore<Contact> _contactsStore;
        public AddContactCommandHandler(IContactsDbContext context, IStore<Contact> contactsStore)
        {
            _context = context;
            _contactsStore = contactsStore;
        }
        public async Task Handle(AddContactCommand request, CancellationToken cancellationToken)
        {
            request.Contact.Created = DateTime.UtcNow;

            await _context.Contacts.AddAsync(request.Contact);
            _context.SaveChanges();

            var savedEntity = _context.Contacts.FirstOrDefault(q => q.Id == request.Contact.Id);
            _contactsStore.AddItem(savedEntity);
        }
    }
}
