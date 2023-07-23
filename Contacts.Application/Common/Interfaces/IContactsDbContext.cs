using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Application.Common.Interfaces
{
    public interface IContactsDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
