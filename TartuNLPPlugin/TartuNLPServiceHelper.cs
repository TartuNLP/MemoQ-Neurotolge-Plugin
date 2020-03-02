﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using TartuNLPInterface;

namespace TartuNLP
{
    /// <summary>
    /// Helper class to be able to communicate with the web service.
    /// </summary>
    /// <remarks>
    /// Implementation checklist:
    ///     - The MTException class is used to wrap the original exceptions occurred during the translation.
    ///     - All allocated resources are disposed correctly in the session.
    /// </remarks>
    internal class TartuNLPServiceHelper
    {
        private static DateTime TokenCodeExpires = DateTime.MinValue;
        //private static string TokenCode;

        //public static IMTService getNewProxy()
        //{
        //    var epAddr = new EndpointAddress("net.tcp://localhost:8733/MTService/DummyService");
        //    return ChannelFactory<IMTService>.CreateChannel(new NetTcpBinding(), epAddr);
        //}

        /// <summary>
        /// Gets the valid token code.
        /// </summary>
        /// <returns>The token code.</returns>
        //public static string GetTokenCode(TartuNLPOptions options)
        //{
        //    if (TokenCodeExpires < DateTime.Now)
        //    {
        //        // refresh the token code
        //        // Always dispose allocated resources
        //        var proxy = getNewProxy();
        //        using (proxy as IDisposable)
        //        {
        //            TokenCode = proxy.Login(options.SecureSettings.URL, options.SecureSettings.Auth);
        //            TokenCodeExpires = DateTime.Now.AddMinutes(1);
        //        }
        //    }

        //    return TokenCode;
        //}
        public static IDictionary<string,string[]> getConfig(string url, string auth)
        {
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent("application/json");
            HttpResponseMessage response = client.PostAsync(url + "conf?auth=" + auth, inputContent).Result;
            var JSONResponse = new JavaScriptSerializer().Deserialize<ConfigJSON>(response.Content.ReadAsStringAsync().Result);
            return JSONResponse.lang_dom_sup;

        }

        /// <summary>
        /// Calls the web service's login method.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token code.</returns>
        //public static string Login(string userName, string password)
        //{
        //    // Always dispose allocated resources
        //    var proxy = getNewProxy();
        //    using (proxy as IDisposable)
        //    {
        //        return proxy.Login(userName, password);
        //    }
        //}

        /// <summary>
        /// Lists the supported languages of the dummy MT service.
        /// </summary>
        /// <returns>The list of the supported languages.</returns>
        public static List<string> ListSupportedLanguages(TartuNLPOptions options)
        {
            return options.GeneralSettings.SupportedLanguages.ToList();
        }

        /// <summary>
        /// Lists the supported languages of the dummy MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <returns>The list of the supported languages.</returns>
        //public static List<string> ListSupportedLanguages(string tokenCode)
        //{
        //    //// Always dispose allocated resources
        //    //var proxy = getNewProxy();
        //    //using (proxy as IDisposable)
        //    //{
        //    //    string[] supportedLanguages = proxy.ListSupportedLanguages(tokenCode).ToArray();
        //    //    return supportedLanguages.ToList();
        //    //}
        //}

        /// <summary>
        /// Translates a single string with the help of the dummy MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The string to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The translated string.</returns>
        public static string Translate(TartuNLPOptions options, string input, string srcLangCode, string trgLangCode)
        {
            // Always dispose allocated resources
            // var proxy = getNewProxy();
            // using (proxy as IDisposable)
            //{
            //    string result = proxy.Translate(GetTokenCode(options), input, srcLangCode, trgLangCode);
            //    return result;
            //}
            if (input.Contains("|"))
                input = StringReplace(input, "|", "&pipe;");
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent("application/json");
            HttpResponseMessage response = client.PostAsync(options.SecureSettings.URL + "?src=" + HttpUtility.UrlEncode(input) + "&engine=etenlv&auth=" + options.SecureSettings.Auth + "&conf=" + trgLangCode + ","+options.GeneralSettings.SelectedDomain, inputContent).Result;
            var JSONResponse = new JavaScriptSerializer().Deserialize<JSONResponse>(response.Content.ReadAsStringAsync().Result);
            if (JSONResponse.tgt.Contains("&pipe;"))
                return StringReplace(JSONResponse.tgt, "&pipe;", "|");
            else
                return JSONResponse.tgt;
        }

        /// <summary>
        /// Translates multiple strings with the help of the dummy MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The strings to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The translated strings.</returns>
        public static List<String>  BatchTranslate(TartuNLPOptions options, List<string> input, string srcLangCode, string trgLangCode)
        {
           
            for (int line = 0; line < input.Count; line++)
            {
                if (input[line].Contains("|"))
                    input[line] = StringReplace(input[line],"|","&pipe;");  
            }
            string batchInput = string.Join("|", input);
            List<string> translation = new List<string> { };
            HttpClient client = new HttpClient();
            HttpContent inputContent = new StringContent("application/json");
            HttpResponseMessage response = client.PostAsync(options.SecureSettings.URL + "?src=" + HttpUtility.UrlEncode(batchInput) + "&engine=etenlv&auth=" + options.SecureSettings.Auth + "&conf=" + trgLangCode + "," + options.GeneralSettings.SelectedDomain, inputContent).Result;
            var JSONResponse = new JavaScriptSerializer().Deserialize<JSONResponse>(response.Content.ReadAsStringAsync().Result);
            translation = JSONResponse.tgt.Split('|').ToList();
            for (int line = 0; line < translation.Count; line++)
            {
                if (translation[line].Contains("&pipe;"))
                    translation[line] = StringReplace(translation[line], "&pipe;","|");
            }

            return translation;
        }

        public static string StringReplace(string text, string oldString, string newString) {

            return text.Replace(oldString, newString);
        }

        /// <summary>
        /// Stores a single string pair as translation with the help of the dummy MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="source">The source string.</param>
        /// <param name="target">The target string.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        public static void StoreTranslation(TartuNLPOptions options, string source, string target, string srcLangCode, string trgLangCode)
        {
            // Always dispose allocated resources
            //var proxy = getNewProxy();
            //using (proxy as IDisposable)
            //{
            //    proxy.StoreTranslation(GetTokenCode(options), source, target, srcLangCode, trgLangCode);
            //}
        }

        /// <summary>
        /// Stores multiple string pairs as translation with the help of the dummy MT service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="sources">The source strings.</param>
        /// <param name="targets">The target strings.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The indices of the translation units that were succesfully stored.</returns>
        public static int[] BatchStoreTranslation(TartuNLPOptions options, List<string> sources, List<string> targets, string srcLangCode, string trgLangCode)
        {
            //// Always dispose allocated resources
            //var proxy = getNewProxy();
            //using (proxy as IDisposable)
            //{
            //    return proxy.BatchStoreTranslation(GetTokenCode(options), sources, targets, srcLangCode, trgLangCode);
            //}
            return new int[0];
        }

       
    }
}
