using Contacts.Application.Common;
using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using System.Runtime.InteropServices;

namespace Contacts.Application.Contacts.Commands.AddContact
{
    public class ApiAddContactCommand : IRequest<ResponseDTO<Contact>>
    {
        public Contact Contact { get; set; }
    }
    public class ApiAddContactCommandHandler : IRequestHandler<ApiAddContactCommand, ResponseDTO<Contact>>
    {
        private readonly IContactsDbContext _context;

        public ApiAddContactCommandHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<Contact>> Handle(ApiAddContactCommand request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO<Contact>();
            try
            {
                request.Contact.Created = DateTime.UtcNow;
                var result = await _context.Contacts.AddAsync(request.Contact);
                _context.SaveChanges();

                response.Success = true;
                response.Content = result.Entity;
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
