using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistance
{
    /// <summary>
    /// Класс используется для создания миграций
    /// </summary>
    public class ContactsContextFactory : IDesignTimeDbContextFactory<ContactsDbContext>
    {
        public ContactsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContactsDbContext>();
            optionsBuilder.UseNpgsql("");
            return new ContactsDbContext(optionsBuilder.Options);
        }
    }
}
