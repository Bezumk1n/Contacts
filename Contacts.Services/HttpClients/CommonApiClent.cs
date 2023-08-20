using Contacts.Application.Common;
using Contacts.Application.Common.Interfaces;
using Contacts.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Services.HttpClients
{
    public class CommonApiClent : ICommonApiClient
    {
        private readonly HttpClient _client;
        public CommonApiClent(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<T> GetData<T>(string uri)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<T> PostData<T>(string uri, T data)
        {
            try
            {
                var content = CreateStringContent<T>(data);
                var response = await _client.PostAsync(uri, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<ResponseDTO<T>>(responseContent);
                return deserialized.Content;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task<T> PutData<T>(string uri, T data)
        {
            try
            {
                var content = CreateStringContent<T>(data);
                var response = await _client.PutAsync(uri, content);

                var responseContent = await response.Content.ReadAsStringAsync();
                var deserialized = JsonConvert.DeserializeObject<ResponseDTO<T>>(responseContent);
                return deserialized.Content;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        public async Task RemoveData(string uri)
        {
            try
            {
                await _client.DeleteAsync(uri);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        private StringContent CreateStringContent<T>(T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return content;
        }
    }
}
