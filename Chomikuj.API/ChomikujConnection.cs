using Chomikuj.API.Extensions;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web;

namespace Chomikuj.API
{
    public class ChomikujConnection : ChomikujFunctions, IChomikujConnection, IDisposable
    {
        private readonly string chomikujUrl = "http://chomikuj.pl/";
        private WebClientWithCookies webClient;
        private ChomikujRequestHeadresSet requestHeadersSet;

        public ChomikujConnection(string username, string password)
        {
            Open(username, password);
        }

        public bool Open(string username, string password)
        {
            webClient = new WebClientWithCookies();
            requestHeadersSet = new ChomikujRequestHeadresSet(webClient, chomikujUrl);

            return Login(username, password);
        }

        public void Close()
        {
            Logout();
        }

        [Description("Returns true if login successed or false if login failed.")]
        private bool Login(string username, string password)
        {
            requestHeadersSet.FirstRequest();
            string[] tokens = GetTwoRequestVerificationTokensForLogin();

            requestHeadersSet.LoginRequest();
            string parametersString = String.Format("__RequestVerificationToken={0}&ReturnUrl={1}&Login={2}&Password={3}&__RequestVerificationToken={4}",
                                                    tokens[0], "", username, password, tokens[1]);

            string responseString = webClient.UploadString(chomikujUrl + "action/Login/TopBarLogin", parametersString);

            try
            {
                dynamic responseObject = JObject.Parse(responseString);
                return (responseObject.Data.LoggedIn == null) ? false : true;
            }
            catch
            {
                return false;
            }
        }

        private void Logout()
        {
            requestHeadersSet.LogoutRequest();
            string parametersString = "redirect=%2F&logout.x=18&logout.y=28";
            webClient.UploadString(chomikujUrl + "action/Login/LogOut", parametersString);
            webClient = null;
        }

        private string[] GetTwoRequestVerificationTokensForLogin()
        {
            string[] tokens = new string[2];
            string responseString = webClient.DownloadString(chomikujUrl);

            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(responseString);

            tokens[0] = htmlDoc.DocumentNode
                           .SelectSingleNode("//form/input[@name='__RequestVerificationToken']")
                           .Attributes["value"].Value;

            tokens[1] = htmlDoc.DocumentNode
               .SelectSingleNode("//div[@class='mainContent']/input[@name='__RequestVerificationToken']")
               .Attributes["value"].Value;

            tokens[0] = HttpUtility.UrlEncode(tokens[0]);
            tokens[1] = HttpUtility.UrlEncode(tokens[1]);

            return tokens;
        }

        public void Dispose()
        {
            Logout();
        }
    }
}
