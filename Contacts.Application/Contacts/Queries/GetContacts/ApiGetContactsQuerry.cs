using Contacts.Application.Common;
using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Contacts.Queries.GetContacts
{
    public class ApiGetContactsQuerry : IRequest<ResponseDTO<ContactListVM>>
    {
    }
    public class ApiGetContactsQuerryHandler : IRequestHandler<ApiGetContactsQuerry, ResponseDTO<ContactListVM>>
    {
        private readonly IContactsDbContext _context;

        public ApiGetContactsQuerryHandler(IContactsDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO<ContactListVM>> Handle(ApiGetContactsQuerry request, CancellationToken cancellationToken)
        {
            var response = new ResponseDTO<ContactListVM>();
            try
            {
                var querry = _context.Contacts.AsNoTracking();

                response.Success = true;
                response.Content = new ContactListVM()
                {
                    Contacts = querry.ToArray(),
                };
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
