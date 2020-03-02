﻿using System.Collections.Generic;
using MemoQ.MTInterfaces;

namespace TartuNLP

{
    /// <summary>
    /// Singleton helper class to be able to localize the plugin's textual information.
    /// </summary>
    internal class LocalizationHelper
    {
        /// <summary>
        /// The default text to be used when the IEnvironment.GetResourceString returns with null.
        /// </summary>
        private Dictionary<string, string> defaultTexts = new Dictionary<string, string>()
        {
            { "OptionsFormCaption", "Tartu NLP plugin settings" },
            { "URLLabelText", "URL" },
            { "AuthLabelText", "Auth" },
            { "RetrieveLanguagesLinkText", "Check login and retrieve language information"},
            { "SupportedLanguagesLabelText", "Supported languages" },
            { "OkButtonText", "OK" },
            { "CancelButtonText", "Cancel" },
            { "HelpButtonText", "Help" },
            { "CommunicationErrorCaption", "Login error" },
            { "CommunicationErrorText", "There was an error during the communication with the service.\n\n{0}" },
            { "InvalidURLorAuthCaption", "Update error" },
            { "InvalidURLorAuthText", "Invalid URL or Auth." },
            { "NetworkError", "A network error occured ({0})" },
            //{ "PlainTextOnly", "Use plain text only" },
            //{ "TextAndFormatting", "Use text and formatting" },
            { "FormattingAndTags", "Use both formatting and tags" },
            { "TagsAndFormattingLabelText", "Tags and formatting"},
        };

        /// <summary>
        /// The singleton instance of the localization helper.
        /// </summary>
        private static LocalizationHelper instance = new LocalizationHelper();

        /// <summary>
        /// Private constructor to avoid multiple instances.
        /// </summary>
        private LocalizationHelper()
        { }

        /// <summary>
        /// The environment to be used to get localized texts from memoQ.
        /// </summary>
        private IEnvironment environment;

        /// <summary>
        /// The singleton instance of the localization helper.
        /// </summary>
        public static LocalizationHelper Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// Sets the environment to be able to get localized texts.
        /// </summary>
        /// <param name="environment"></param>
        public void SetEnvironment(IEnvironment environment)
        {
            this.environment = environment;
        }

        /// <summary>
        /// Gets the localized text belonging to the specified key.
        /// </summary>
        public string GetResourceString(string key)
        {
            // try to get the localized text from the environment
            string localizedText = "";
                //environment.GetResourceString(TartuNLPPluginDirector.PluginId,key);

            // use the default texts if the environment returns with null
            if (string.IsNullOrEmpty(localizedText))
                localizedText = defaultTexts[key];

            return localizedText;
        }
    }
}
