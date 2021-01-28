using MemoQ.MTInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TartuNLP
{
    /// <summary>
    /// This class represents the options form of the MT plugin.
    /// </summary>
    /// <remarks>
    /// Implementation checklist:
    ///     - 	There is a configuration dialog, where the user is able to configure the plugin.
    ///     -   The user cannot save the settings until all of the mandatory parameters were not configured correctly.
    ///     -   The dialog collects the user modifications in the memory and saves only when the user OKs the dialog.
    ///     - 	The dialog does not call any blocking service in the user interface thread; it has to use background threads.
    ///     -   Check UI so that it is displayed correctly at high DPI settings.
    /// </remarks>
    public partial class TartuNLPOptionsForm : Form
    {
        private IEnvironment environment;

        private class LanguageDomainSupport
        {
            public EngineConf EngineConf;
            public bool UpdateSuccessful;
            public Dictionary<string, string> SupportedDomains;
            public Dictionary<string, Dictionary<string, List<string>>> SupportedLanguages;
            public bool FormattingAndTagUsage;
            public Exception Exception;
        }

        private LanguageDomainSupport languageDomainSupport;

        public TartuNLPOptions Options { get; set; }

        public TartuNLPOptionsForm(IEnvironment environment)
        {
            InitializeComponent();
            this.environment = environment;

            // localization
            localizeContent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            btnUpdate.Enabled = false;
            tbURL.Text = Options.SecureSettings.URL;
            tbAuth.Text = Options.SecureSettings.Auth;
            btnOK.Enabled = !string.IsNullOrEmpty(Options.SecureSettings.URL);
            cbDomain.Items.Clear();
            if (Options.GeneralSettings.EngineConf != null)
            {
                languageDomainSupport = loadEngineConf(Options.GeneralSettings.EngineConf);
                cbDomain.Items.Clear();
                foreach (var domainName in languageDomainSupport.SupportedDomains.Keys)
                {
                    cbDomain.Items.Add(domainName);
                }

                cbDomain.SelectedItem = Options.GeneralSettings.SelectedDomainName;
            }
        }

        private void localizeContent()
        {
            Text = LocalizationHelper.Instance.GetResourceString("OptionsFormCaption");
            lblURL.Text = LocalizationHelper.Instance.GetResourceString("URLLabelText");
            lblAuth.Text = LocalizationHelper.Instance.GetResourceString("AuthLabelText");
            lblSupportedLanguages.Text = LocalizationHelper.Instance.GetResourceString("SupportedLanguagesLabelText");
            btnOK.Text = LocalizationHelper.Instance.GetResourceString("OkButtonText");
            btnCancel.Text = LocalizationHelper.Instance.GetResourceString("CancelButtonText");   
        }

        private void tbURLAuth_TextChanged(object sender, EventArgs e)
        {
            btnUpdate.Enabled = true;
            btnOK.Enabled = false;
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            //setControlsEnabledState(false);
            btnOK.Enabled = false;
            srcLanguages.Items.Clear();
            tgtLanguages.Items.Clear();
            
            // do the update in the background
            languageDomainSupport = await updateConfig(tbURL.Text, tbAuth.Text);
            handleUpdateFinished();
        }

        private LanguageDomainSupport loadEngineConf(EngineConf engineConf)
        {
            var config = new LanguageDomainSupport
            {
                EngineConf = engineConf,
                UpdateSuccessful = true,
                FormattingAndTagUsage = engineConf.xml_support,
                SupportedDomains = new Dictionary<string, string>(),
                SupportedLanguages = new Dictionary<string, Dictionary<string, List<string>>>()
            };
            
            foreach (var domain in engineConf.domains)
            {
                config.SupportedDomains.Add(domain.name, domain.code);
                config.SupportedLanguages.Add(domain.code, new Dictionary<string, List<string>>());
                foreach (var language in domain.languages)
                {
                    var languagePair = language.Split('-');
                    if (!config.SupportedLanguages[domain.code].ContainsKey(languagePair[0]))
                    {
                        config.SupportedLanguages[domain.code].Add(languagePair[0], new List<string>());
                    }

                    config.SupportedLanguages[domain.code][languagePair[0]].Add(languagePair[1]);
                }
            }

            return config;
        }

        private async Task<LanguageDomainSupport> updateConfig(string url, string auth) {
            var config = new LanguageDomainSupport();
            try
            {
                // try to get Configuration
                // Do not call any blocking service in the user interface thread; it has to use background threads.
                var engineConf = await Task.Run(() => TartuNLPServiceHelper.getConfig(url, auth));

                if (engineConf == null)
                {
                    //invalid user name or password
                    config.UpdateSuccessful = false;
                }
                else
                {
                    config = loadEngineConf(engineConf);
                }
            }
            catch (Exception ex)
            {
                config.Exception = ex;
            }

            return config;
        }

        private void handleUpdateFinished()
        {
            // it is possible that the form has disposed during the background operation (e.g. the user clicked on the cancel button)
            if (!IsDisposed)
            {
                if (languageDomainSupport.Exception != null)
                {
                    // there was an error, display for the user
                    string caption = LocalizationHelper.Instance.GetResourceString("CommunicationErrorCaption");
                    string text = LocalizationHelper.Instance.GetResourceString("CommunicationErrorText");
                    MessageBox.Show(this, string.Format(text, languageDomainSupport.Exception.Message), caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (!languageDomainSupport.UpdateSuccessful)
                {
                    // the URL or Auth is invalid, display for the user
                    var caption = LocalizationHelper.Instance.GetResourceString("InvalidURLorAuthCaption");
                    var text = LocalizationHelper.Instance.GetResourceString("InvalidURLorAuthText");
                    MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // we have managed to get the supported Domain, display them in the combo box
                    cbDomain.Items.Clear();
                    foreach (var domainName in languageDomainSupport.SupportedDomains.Keys)
                    {
                       cbDomain.Items.Add(domainName);
                    }
                    cbDomain.SelectedIndex = 0;

                }   
            }
        }

        private void cbDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            srcLanguages.Items.Clear();
            if (languageDomainSupport != null)
            {
                var domain = languageDomainSupport.SupportedDomains[cbDomain.SelectedItem.ToString()];
                srcLanguages.Items.Clear();
                foreach (var language in languageDomainSupport.SupportedLanguages[domain])
                {
                    srcLanguages.Items.Add(language.Key);
                }
                srcLanguages.SelectedIndex = 0;
                
                btnOK.Enabled = languageDomainSupport.SupportedLanguages[domain].Count > 0;
            }
        }
        
        private void srcLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            tgtLanguages.Items.Clear();
            if (languageDomainSupport != null)
            {
                var domain = languageDomainSupport.SupportedDomains[cbDomain.SelectedItem.ToString()];
                tgtLanguages.Items.Clear();
                foreach (var language in languageDomainSupport.SupportedLanguages[domain][srcLanguages.SelectedItem.ToString()])
                {
                    tgtLanguages.Items.Add(language);
                }
            }
        }


        private void TartuNLPOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                // if there was a modification, we have to save the changes
                Options.SecureSettings.URL = tbURL.Text;
                Options.SecureSettings.Auth = tbAuth.Text;
                if (languageDomainSupport != null)
                {
                    var domain = languageDomainSupport.SupportedDomains[cbDomain.SelectedItem.ToString()];
                    Options.GeneralSettings.EngineConf = languageDomainSupport.EngineConf;
                    var languagePairs = new List<(string, string)>();
                    foreach (var languagePair in languageDomainSupport.SupportedLanguages[domain])
                    {
                        foreach (var targetLang in languagePair.Value)
                        {
                            languagePairs.Add((languagePair.Key, targetLang));
                        }
                    }
                    Options.GeneralSettings.SupportedLanguages = languagePairs.ToArray();
                    Options.GeneralSettings.SelectedDomainCode = domain;
                    Options.GeneralSettings.SelectedDomainName = cbDomain.SelectedItem.ToString();
                    if (languageDomainSupport.FormattingAndTagUsage)
                    {
                        Options.GeneralSettings.FormattingAndTagUsage = FormattingAndTagsUsageOption.BothFormattingAndTags;
                    }
                }
                srcLanguages.Items.Clear();
                tgtLanguages.Items.Clear();
                cbDomain.Items.Clear();
            }
        }


        private void TartuNLPOptionsForm_Load(object sender, EventArgs e)
        {}
    }
}
