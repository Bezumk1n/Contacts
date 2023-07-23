using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using Contacts.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Persistance
{
    public class ContactsDbContext : DbContext, IContactsDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
