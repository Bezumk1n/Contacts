using Contacts.Application.Common;
using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class ApiUpdateContactCommand : IRequest<ResponseDTO<Contact>>
    { 
        public Contact Contact { get; set; }
    }
    public class ApiUpdateContactCommandHandler : IRequestHandler<ApiUpdateContactCommand, ResponseDTO<Contact>>
    {
        private readonly IContactsDbContext _context;

        public ApiUpdateContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<Contact>> Handle(ApiUpdateContactCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO<Contact>();
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

                response.Content = await _context.Contacts.FindAsync(request.Contact.Id);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}
