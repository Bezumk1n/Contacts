using Contacts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Contacts.Queries.GetContacts
{
    public class ContactListVM
    {
        public IList<Contact> Contacts { get; set; }
    }
}
