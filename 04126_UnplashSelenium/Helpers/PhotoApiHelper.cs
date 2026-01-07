using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace _04126_UnplashSelenium.Helpers
{
    internal class PhotoApiHelper : BaseApiHelper
    {
        public PhotoApiHelper(string accessToken, string baseUrl)
            : base(accessToken, baseUrl)
        {
        }

        public async Task<List<string>> GetLikedPhotoIds(string username)
        {
            try
            {
                var url = BuildUrl($"/users/{username}/likes");
                var response = await HttpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var photos = JsonConvert.DeserializeObject<List<PhotoDto>>(json);

                return photos?.Select(p => p.Id).ToList() ?? new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Get liked photos error: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<bool> DeleteLikedPhoto(string photoId)
        {
            try
            {
                var url = BuildUrl($"/napi/photos/{photoId}/bookmark");
                var response = await HttpClient.DeleteAsync(url);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete liked photo error: {ex.Message}");
                return false;
            }
        }

        internal class PhotoDto
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }
    }
}
