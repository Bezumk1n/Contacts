using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class ApiUpdateContactCommand : IRequest<Contact>
    { 
        public Contact Contact { get; set; }
    }
    public class ApiUpdateContactCommandHandler : IRequestHandler<ApiUpdateContactCommand, Contact>
    {
        private readonly IContactsDbContext _context;

        public ApiUpdateContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<Contact> Handle(ApiUpdateContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _context.Contacts.FindAsync(request.Contact.Id);

                if (entity.Name != request.Contact.Name) entity.Name = request.Contact.Name;
                if (entity.Surname != request.Contact.Surname) entity.Surname = request.Contact.Surname;
                if (entity.Address != request.Contact.Address) entity.Address = request.Contact.Address;
                if (entity.Email != request.Contact.Email) entity.Email = request.Contact.Email;
                if (entity.Description != request.Contact.Description) entity.Description = request.Contact.Description;
                if (entity.PhoneNumber != request.Contact.PhoneNumber) entity.PhoneNumber = request.Contact.PhoneNumber;

                _context.SaveChanges();

                return await _context.Contacts.FindAsync(request.Contact.Id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            

        }
    }
}
