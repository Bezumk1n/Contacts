using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Contacts.Application.Contacts.Commands.AddContact
{
    public class ApiAddContactCommand : IRequest<Contact>
    {
        public Contact Contact { get; set; }
    }
    public class ApiAddContactCommandHandler : IRequestHandler<ApiAddContactCommand, Contact>
    {
        private readonly IContactsDbContext _context;

        public ApiAddContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> Handle(ApiAddContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                request.Contact.Created = DateTime.UtcNow;
                var result = await _context.Contacts.AddAsync(request.Contact);
                _context.SaveChanges();
                return result.Entity;

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
