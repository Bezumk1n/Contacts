using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using Contacts.Services.HttpClients;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.ContactsServices
{
    public class ContactsApiService : IContactsApiService
    {
        private readonly CommonApiClent _commonApiClent;

        public ContactsApiService(CommonApiClent commonApiClent)
        {
            _commonApiClent = commonApiClent;
        }
        public async Task<Contact> AddContact(Contact contact)
        {
            try
            {
                //string uri = "http://192.168.0.175:5000/api/v1/Contacts/AddContact";
                string uriLocal = "http://localhost:5000/api/v1/Contacts/AddContact";
                return await _commonApiClent.PostData<Contact>(uriLocal, contact);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //public async Task<Contact> UpdateContact(Contact contact)
        //{
        //    string uri = "http://192.168.0.175:5000/api/v1/Contacts/UpdateContact";
        //}
        //public async Task<IEnumerable<Contact>> GetContactsContact()
        //{
        //    string uri = "http://192.168.0.175:5000/api/v1/Contacts/GetContacts";
        //}
        public async Task RemoveContact(Guid id)
        {
            try
            {
                //string uri = "http://192.168.0.175:5000/api/v1/Contacts/RemoveContact";
                string uriLocal = "http://localhost:5000/api/v1/Contacts/RemoveContact";
                await _commonApiClent.PostData<Guid>(uriLocal, id);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
