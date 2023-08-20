using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Application.Common.Interfaces
{
    public interface ICommonApiClient
    {
        Task<T> GetData<T>(string uri);
        Task<T> PostData<T>(string uri, T data);
    }
}
