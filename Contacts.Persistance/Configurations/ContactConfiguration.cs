using Contacts.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Persistance.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(contact => contact.Id);
            builder.HasIndex(contact => contact.Id).IsUnique();
            builder.Property(contact => contact.Name).HasMaxLength(50).IsRequired();
            builder.Property(contact => contact.Surname).HasMaxLength(50);
            builder.Property(contact => contact.PhoneNumber).HasMaxLength(20);
            builder.Property(contact => contact.Email).HasMaxLength(100);
            builder.Property(contact => contact.Address).HasMaxLength(100);
            builder.Property(contact => contact.Description).HasMaxLength(2000);
        }
    }
}
