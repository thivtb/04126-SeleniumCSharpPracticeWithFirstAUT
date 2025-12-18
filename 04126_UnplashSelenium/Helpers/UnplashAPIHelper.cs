using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace _04126_UnplashSelenium.Helpers
{
    internal class UnplashAPIHelper
    {
        HttpClient httpClient;
        public UnplashAPIHelper(string accessToken)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public async Task<bool> DeleteCollectionByTitle(string username, string title, string baseUrl)
        {
            var collections = await GetUserCollections(username, baseUrl);
            var collection = collections.FirstOrDefault(c =>
                c.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (collection != null)
            {
                Console.WriteLine($"Found collection: '{collection.Title}' (ID: {collection.Id})");
                return await DeleteCollection(collection.Id, baseUrl);
            }
            else
            {
                Console.WriteLine($"Collection not found: '{title}'");
                return false;
            }
        }

        private async Task<List<CollectionDto>> GetUserCollections(string username, string baseUrl)
        {
            try
            {
                var url = $"{baseUrl}/users/{username}/collections";
                Console.WriteLine(url);
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var collections = JsonConvert.DeserializeObject<List<CollectionDto>>(content);

                return collections ?? new List<CollectionDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting collections: {ex.Message}");
                return new List<CollectionDto>();
            }
        }

        private async Task<bool> DeleteCollection(string collectionId, string baseUrl)
        {
            try
            {
                var url = $"{baseUrl}/collections/{collectionId}";
                var response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Deleted collection ID: {collectionId}");
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed: {response.StatusCode} - {error}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return false;
            }
        }

        internal class CollectionDto
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("published_at")]
            public DateTime PublishedAt { get; set; }
        }
    }
}
