using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;


namespace TartuNLP
{
    /// <summary>
    /// Helper class to be able to communicate with the web service.
    /// </summary>
    internal static class TartuNLPServiceHelper
    {
        public static EngineConf getConfig(string url, string auth)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", auth);
            client.DefaultRequestHeaders.Add("application", "MemoQ");
            var response = client.GetAsync(url).Result;
            var engineConf = new JavaScriptSerializer().Deserialize<EngineConf>(response.Content.ReadAsStringAsync().Result);
            return engineConf;

        }

        /// <summary>
        /// Translates a single string with the help of the MT service.
        /// </summary>
        /// <param name="options">Current plugin configuration.</param>
        /// <param name="input">The string to translate.</param>
        /// <param name="srcLangCode">The source language code.</param>
        /// <param name="trgLangCode">The target language code.</param>
        /// <returns>The translated string.</returns>
        public static List<string> Translate(TartuNLPOptions options, List<string> input, string srcLangCode, string trgLangCode)
        {
            if(trgLangCode.Contains("-"))
                trgLangCode = trgLangCode.Split('-')[0];
            if(srcLangCode.Contains("-"))
                srcLangCode = srcLangCode.Split('-')[0];

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", options.SecureSettings.Auth);
            client.DefaultRequestHeaders.Add("application", "MemoQ");
            var content = new Input
            {
                text = input,
                src = srcLangCode,
                tgt = trgLangCode,
                domain = options.GeneralSettings.SelectedDomainCode
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(content);
            var strContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = client.PostAsync(options.SecureSettings.URL, strContent).Result;
            var JSONResponse = new JavaScriptSerializer().Deserialize<JSONResponse>(response.Content.ReadAsStringAsync().Result);
            return JSONResponse.result;
        }
    }

    internal class Input
    {
        public List<string> text;
        public string src;
        public string tgt;
        public string domain;
    }
}
