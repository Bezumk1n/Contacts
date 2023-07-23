using Contacts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public override string ToString()
        {
            return $"{Name}{Surname}";
        }
        public Contact Clone()
        {
            Contact contact = new Contact();
            contact.Id = Id;
            contact.Created = Created;
            contact.Name = Name;
            contact.Surname = Surname;
            contact.PhoneNumber = PhoneNumber;
            contact.Email = Email;
            contact.Address = Address;
            contact.Description = Description;
            return contact;
        }
    }
}
