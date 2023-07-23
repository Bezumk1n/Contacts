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
        private readonly IContactsDbContext _context;
        private readonly IStore<Contact> _contactsStore;

        public GetContactsQuerryHandler(IContactsDbContext context, IStore<Contact> contactsStore)
        {
            _context = context;
            _contactsStore = contactsStore;
        }
        public async Task Handle(GetContactsQuerry request, CancellationToken cancellationToken)
        {
            var querry = _context.Contacts.AsNoTracking();
            _contactsStore.AddItems(querry.ToList());
        }
    }
}
