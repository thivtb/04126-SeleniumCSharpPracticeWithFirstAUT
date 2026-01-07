using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace _04126_UnplashSelenium.Helpers
{
    internal class BaseApiHelper
    {
        protected HttpClient HttpClient;
        protected string BaseUrl;

        public BaseApiHelper(string accessToken, string baseUrl)
        {
            BaseUrl = baseUrl.TrimEnd('/');
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        protected string BuildUrl(string relativePath)
        {
            return $"{BaseUrl}/{relativePath.TrimStart('/')}";
        }

        public void Dispose()
        {
            HttpClient?.Dispose();
        }
    }
}
