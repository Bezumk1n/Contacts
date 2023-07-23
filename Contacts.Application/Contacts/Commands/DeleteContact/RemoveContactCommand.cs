using Contacts.Application.Common.Interfaces;
using Contacts.Application.Contacts.Commands.AddContact;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
