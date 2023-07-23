using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
