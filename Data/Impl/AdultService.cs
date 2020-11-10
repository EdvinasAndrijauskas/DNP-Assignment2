using System;
using System.Collections.Generic;
using Models;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Managing_Adults.Data.Impl
{
    public class AdultService : IAdultService
    {
        private string uri = "http://dnp.metamate.me";
        private readonly HttpClient client;

        public AdultService() {
            client = new HttpClient();
        }
        public async Task<IList<Adult>> GetAdultAsync() {
            Task<string> stringAsync = client.GetStringAsync(uri + "/Adults");
            string message = await stringAsync;
            List<Adult> result = JsonSerializer.Deserialize<List<Adult>>(message);
            return result;
        }

        public async Task AddAdult(Adult adult)
        {
            string todoSerialized = JsonSerializer.Serialize(adult);
            HttpContent content = new StringContent(todoSerialized,
                Encoding.UTF8,
                "application/json");
            await client.PutAsync("http://dnp.metamate.me/Adults", content);
        }


        public async Task RemoveAdultAsync(int adultId) {
            await client.DeleteAsync($"{uri}/Adults/{adultId}");
        }

 

    }
}

