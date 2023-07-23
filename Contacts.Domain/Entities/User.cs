using Contacts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Domain.Entities
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
    }
}
