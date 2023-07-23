using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Common.Interfaces
{
    public interface IContactsDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
