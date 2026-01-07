using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _04126_UnplashSelenium.Helpers
{
    internal class CollectionApiHelper : BaseApiHelper
    {
        public CollectionApiHelper(string accessToken, string baseUrl)
            : base(accessToken, baseUrl)
        {
        }

        public async Task<bool> DeleteCollectionByTitle(string username, string title)
        {
            var collections = await GetUserCollections(username);
            var collection = collections.FirstOrDefault(c =>
                c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (collection == null)
            {
                Console.WriteLine($"Collection not found: {title}");
                return false;
            }

            return await DeleteCollection(collection.Id);
        }

        public async Task<List<CollectionDto>> GetUserCollections(string username)
        {
            try
            {
                var url = BuildUrl($"/users/{username}/collections");
                var response = await HttpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<CollectionDto>>(json)
                       ?? new List<CollectionDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get collections error: {ex.Message}");
                return new List<CollectionDto>();
            }
        }

        private async Task<bool> DeleteCollection(string collectionId)
        {
            try
            {
                var url = BuildUrl($"/collections/{collectionId}");
                var response = await HttpClient.DeleteAsync(url);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete collection error: {ex.Message}");
                return false;
            }
        }

        internal class CollectionDto
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }
        }
    }
}
