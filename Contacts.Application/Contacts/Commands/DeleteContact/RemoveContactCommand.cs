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
        private readonly IContactsApiService _contactsApiService;

        public RemoveContactCommandHandler(IContactsDbContext context, IStore<Contact> contactsStore, IContactsApiService contactsApiService)
        {
            _context = context;
            _contactsStore = contactsStore;
            _contactsApiService = contactsApiService;
        }
        public async Task Handle(RemoveContactCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _contactsApiService.RemoveContact(request.Id);
                _contactsStore.RemoveItem(request.Id);

            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
