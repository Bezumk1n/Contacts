using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Contacts.Queries.GetContacts
{
    public class ApiGetContactsQuerry : IRequest<ContactListVM>
    {
    }
    public class ApiGetContactsQuerryHandler : IRequestHandler<ApiGetContactsQuerry, ContactListVM>
    {
        private readonly IContactsDbContext _context;

        public ApiGetContactsQuerryHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<ContactListVM> Handle(ApiGetContactsQuerry request, CancellationToken cancellationToken)
        {
            var querry = _context.Contacts.AsNoTracking();
            var result = new ContactListVM
            {
                Contacts = querry.ToList()
            };
            return result;
        }
    }
}
