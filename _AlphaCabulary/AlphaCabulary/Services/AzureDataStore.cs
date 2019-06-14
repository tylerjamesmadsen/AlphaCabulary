using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AlphaCabulary.Models;

namespace AlphaCabulary.Services
{
    public class AzureDataStore : IDataStore<Item>
    {
        private HttpClient _client;
        private IEnumerable<Item> _items;

        public AzureDataStore()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

            _items = new List<Item>();
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            if (forceRefresh)
            {
                string json = await _client.GetStringAsync($"api/item");
                _items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
            }

            return _items;
        }

        public async Task<Item> GetItemAsync(string id)
        {
            if (id != null)
            {
                string json = await _client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            if (item == null)
                return false;

            string serializedItem = JsonConvert.SerializeObject(item);

            HttpResponseMessage response = await _client.PostAsync($"api/item", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            if (item == null || item.Id == null)
                return false;

            string serializedItem = JsonConvert.SerializeObject(item);
            byte[] buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            HttpResponseMessage response = await _client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return false;

            HttpResponseMessage response = await _client.DeleteAsync($"api/item/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}