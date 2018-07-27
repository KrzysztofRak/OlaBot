using Chomikuj.API.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Chomikuj.API
{
    class ChomikujRequestHeadresSet
    {
        private WebClientWithCookies _webClient;
        private string _chomikujUrl;
        private string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:61.0) Gecko/20100101 Firefox/61.0";

        public ChomikujRequestHeadresSet(WebClientWithCookies webClient, string chomikujUrl)
        {
            _webClient = webClient;
        }

        public void FirstRequest()
        {
            _webClient.Headers.Clear();
            _webClient.Headers.Add("Host", "chomikuj.pl");
            _webClient.Headers.Add("User-Agent", userAgent);
            _webClient.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            _webClient.Headers.Add("Accept-Language", "pl,en-US;q=0.7,en;q=0.3");
            _webClient.Headers.Add("Upgrade-Insecure-Requests", "1");
        }

        public void LoginRequest()
        {
            _webClient.Headers.Clear();
            _webClient.Headers.Add("Host", "chomikuj.pl");
            _webClient.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:61.0) Gecko/20100101 Firefox/61.0");
            _webClient.Headers.Add("Accept", "*/*");
            _webClient.Headers.Add("Accept-Language", "pl,en-US;q=0.7,en;q=0.3");
            _webClient.Headers.Add("Referer", _chomikujUrl);
            _webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            _webClient.Headers.Add("X-Requested-With", "XMLHttpRequest");
        }

        public void LogoutRequest()
        {
            _webClient.Headers.Clear();
            LoginRequest();
            _webClient.Headers.Remove("X-Requested-With");
            _webClient.Headers.Add("Upgrade-Insecure-Requests", "1");
            _webClient.Headers.Set("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        }
    }
}
