using Contacts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Common.Interfaces
{
    public interface IContactsApiService
    {
        Task<Contact> AddContact(Contact contact);
        Task RemoveContact(Guid id);
    }
}
