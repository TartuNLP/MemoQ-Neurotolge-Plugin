using System.Collections.Generic;

namespace TartuNLP
{
    /// <summary>
    /// Class for storing the MT plugin settings.
    /// </summary>
    public class TartuNLPOptions : MemoQ.MTInterfaces.PluginSettingsObject<TartuNLPGeneralSettings, TartuNLPSecureSetting>
    {
        /// <summary>
        /// Create instance by deserializing from provided serialized settings.
        /// </summary>
        public TartuNLPOptions(MemoQ.MTInterfaces.PluginSettings serializedSettings): base(serializedSettings){}

        /// <summary>
        /// Create instance by providing the settings objects.
        /// </summary>
        public TartuNLPOptions(TartuNLPGeneralSettings generalSettings, TartuNLPSecureSetting secureSettings)
            : base(generalSettings, secureSettings){}
    }

    /// <summary>
    /// General settings, content preserved when settings are exported.
    /// </summary>
    public class TartuNLPGeneralSettings
    {
        public EngineConf EngineConf;
        public FormattingAndTagsUsageOption FormattingAndTagUsage = FormattingAndTagsUsageOption.Plaintext;
        public (string, string)[] SupportedLanguages;
        public string SelectedDomainCode;
        public string SelectedDomainName;
    }

    /// <summary>
    /// Settings, whether inline tags and/or formatting should be included in the request sent to the machine translation provider.
    /// </summary>
    public enum FormattingAndTagsUsageOption
    {
        Plaintext,
        BothFormattingAndTags
    }

    /// <summary>
    /// Secure settings, content not preserved when settings leave the machine.
    /// </summary>
    public class TartuNLPSecureSetting
    {
        public string URL = "https://api.tartunlp.ai/translation/v2";
        public string Auth = "public";
    }
}
