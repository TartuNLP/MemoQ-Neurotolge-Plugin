﻿using System;
using System.Collections.Generic;
using System.Threading;
using TartuNLPInterface;

namespace DummyMTServiceWCF
{

    /// <summary>
    /// Dummy web service which can be called by the dummy MT plugin.
    /// </summary>
    /// <remarks>
    /// Implementation checklist:
    ///     - The MTException class is used to wrap the original exceptions occurred during the translation.
    ///     - All allocated resources are disposed correctly in the session.
    /// </remarks>
    public class MTService : IMTService
    {
        public MTService()
        {

            //Uncomment the following line if using designed components
            //InitializeComponent();
        }

        /// <summary>
        /// Call this method to get a token code for the further
        /// calls.Returns the token code is the credentials are
        /// valid (this dummy service allows the login request
        /// if the user name and the password are identical), 
        /// otherwise returns null.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        /// <returns>The token code if the credentials are valid, null otherwise.</returns>
        public string Login(string userName, string password)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            return userName.Equals(password) ? TokenCodeGenerator.Instance.GenerateTokenCode(userName) : null;
        }

        /// <summary>
        /// Call this method to get the supported languages of the service.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <returns>The supported languages.</returns>
        public List<string> ListSupportedLanguages(string tokenCode)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            if (!TokenCodeGenerator.Instance.TokenCodeIsValid(tokenCode))
                return null;

            return new List<string>() { "eng", "fre", "ger", "ita", "por" };
        }

        /// <summary>
        /// Call this method to get the translation for a single string.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The input string.</param>
        /// <param name="srcLangCode">The code of the source language.</param>
        /// <param name="trgLangCode">The code of the target language.</param>
        /// <returns>The translated input string.</returns>
        public string Translate(string tokenCode, string input, string srcLangCode, string trgLangCode)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            if (!TokenCodeGenerator.Instance.TokenCodeIsValid(tokenCode))
                return null;

            string storedTranslation;
            if (StoredTranslations.TryTranslate(input, srcLangCode, trgLangCode, out storedTranslation))
                return storedTranslation;

            return string.Format("{0} -> {1} - {2}", srcLangCode, trgLangCode, input);
        }

        /// <summary>
        /// Call this method to get the translation for multiple strings in batch.
        /// </summary>
        /// <param name="tokenCode">The token code.</param>
        /// <param name="input">The input strings.</param>
        /// <param name="srcLangCode">The code of the source language.</param>
        /// <param name="trgLangCode">The code of the target language.</param>
        /// <returns>The translated input strings.</returns>
        public List<string> BatchTranslate(string tokenCode, List<string> input, string srcLangCode, string trgLangCode)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            if (!TokenCodeGenerator.Instance.TokenCodeIsValid(tokenCode))
                return null;

            string storedTranslation;
            List<string> result = new List<string>();
            foreach (string item in input)
            {
                if (StoredTranslations.TryTranslate(item, srcLangCode, trgLangCode, out storedTranslation))
                    result.Add(storedTranslation);
                else
                    result.Add(string.Format("{0} -> {1} - {2}", srcLangCode, trgLangCode, item));
            }

            return result;
        }

        public void StoreTranslation(string tokenCode, string source, string target, string srcLangCode, string trgLangCode)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            if (!TokenCodeGenerator.Instance.TokenCodeIsValid(tokenCode))
                return;

            StoredTranslations.Store(source, target, srcLangCode, trgLangCode);
        }

        public int[] BatchStoreTranslation(string tokenCode, List<string> sources, List<string> targets, string srcLangCode, string trgLangCode)
        {
            // simulate the network latency
            Thread.Sleep(3000);

            if (!TokenCodeGenerator.Instance.TokenCodeIsValid(tokenCode))
                return new int[0];

            var indicesAdded = new int[sources.Count];
            for (int i = 0; i < sources.Count; ++i)
            {
                StoredTranslations.Store(sources[i], targets[i], srcLangCode, trgLangCode);
                indicesAdded[i] = i;
            }
            return indicesAdded;
        }
    }
}
