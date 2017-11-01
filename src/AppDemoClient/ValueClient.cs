using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AppDemo.Client
{
    public class ValueClient
    {
        public IList<string> GetValues()
        {
            return GetValuesAsync().Result;
        }

        public async Task<IList<string>> GetValuesAsync()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/values");
            if (!response.IsSuccessStatusCode)
            {
                return new List<string>();
            }
          
            return await response.Content.ReadAsAsync<List<string>>();
        }

        public string GetValue(int index)
        {
            return GetValueAsync(index).Result;
        }

        public async Task<string> GetValueAsync(int index)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"api/values/{index}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }

        protected virtual HttpClient GetHttpClient()
        {
            var apiUrl = GetApiUrl();
            var client = new HttpClient { BaseAddress = new Uri(apiUrl) };

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected virtual string GetApiUrl()
        {
            var demoUrl = ConfigurationManager.AppSettings["AppDemo.Url"];
            return demoUrl;
        }
    }
}
