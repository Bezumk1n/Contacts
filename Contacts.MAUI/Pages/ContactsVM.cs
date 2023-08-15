using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using Contacts.MAUI.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.MAUI.Pages
{
    public class ContactsVM : BaseVM
    {
        private readonly ISender _mediator;
        private readonly IStore<Domain.Entities.Contact> _contactsStore;
        private readonly IStore<User> _usersStore;
        public ContactsVM(ISender mediator, IStore<Domain.Entities.Contact> contactsStore, IStore<User> usersStore)
        {
            _mediator = mediator;
            _contactsStore = contactsStore;
            _usersStore = usersStore;
        }
    }
}
