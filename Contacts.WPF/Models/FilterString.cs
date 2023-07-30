using Contacts.WPF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.WPF.Models
{
    public class FilterString
    {
        public string Filter { get; set; }
        public bool IsChecked { get; set; }
        public bool IsAll { get; set; }
        public static FilterString Create(string filter, bool isAll = false)
        {
            FilterString result = new FilterString();
            result.Filter = filter;
            result.IsChecked = isAll; 
            result.IsAll = isAll; 
            return result;
        }
    }
}
