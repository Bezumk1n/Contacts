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
        private readonly IContactsDbContext _context;
        private readonly IStore<Contact> _contactsStore;
        public RemoveContactCommandHandler(IContactsDbContext context, IStore<Contact> contactsStore)
        {
            _context = context;
            _contactsStore = contactsStore;
        }
        public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Contacts.FirstOrDefaultAsync(q => q.Id == request.Id);
            _context.Contacts.Remove(entity);
            _context.SaveChanges();
            _contactsStore.RemoveItem(request.Id);
        }
    }
}
