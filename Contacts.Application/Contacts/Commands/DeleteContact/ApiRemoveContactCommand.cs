using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class ApiRemoveContactCommand : IRequest
    {
        public Guid Id { get; set; }
    }
    public class ApiRemoveContactCommandHandler : IRequestHandler<ApiRemoveContactCommand>
    {
        private readonly IContactsDbContext _context;

        public ApiRemoveContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task Handle(ApiRemoveContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(request.Id);
                _context.Contacts.Remove(contact);
                _context.SaveChanges();

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
