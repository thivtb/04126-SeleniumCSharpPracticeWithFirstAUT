using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c__basic_SD5858_VoThiBeThi_section1.Configs
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UnsplashApiBaseUrl { get; set; }
        public string UnsplashAccessToken { get; set; }
        public string UnsplashUsername { get; set; }

        public string DownloadFolder { get; set; }
        public static TestSettings LoadSettings()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs", "TestData.json");
            var json = File.ReadAllText(path);
            var settings = JsonConvert.DeserializeObject<TestSettings>(json);
            return settings;
        }
    }
}
