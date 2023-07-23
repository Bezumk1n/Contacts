using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Users.Queries
{
    public class GetUserQuerry : IRequest
    {
    }
    public class GetUserQuerryHandler : IRequestHandler<GetUserQuerry>
    {
        private readonly IContactsDbContext _context;
        private readonly IStore<User> _userStore;

        public GetUserQuerryHandler(IContactsDbContext context, IStore<User> userStore)
        {
            _context = context;
            _userStore = userStore;
        }
        public async Task Handle(GetUserQuerry request, CancellationToken cancellationToken)
        {
            var querry = _context.Users.AsNoTracking();
            _userStore.AddItems(querry.ToList());
        }
    }
}
