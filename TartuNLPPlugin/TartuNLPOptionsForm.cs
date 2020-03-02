using MemoQ.MTInterfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TartuNLP
{
    /// <summary>
    /// This class represents the options form of the dummy MT plugin.
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
        private delegate void LoginDelegate(string userName, string password);
        private IEnvironment environment;

        private class LanguageDomainSupport
        {
            public string URL;
            public string Auth;
            public bool UpdateSuccessful;
            public IDictionary<string,string[]> SupportedLanguages;
            public IDictionary<string, string> SupportedDomains;
            public IDictionary<string, string[]> JSON;
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
            this.btnOK.Enabled = !string.IsNullOrEmpty(Options.SecureSettings.URL);
            btnHelp.Enabled = isShowHelpSupported();
            cbFormattingTags.SelectedIndex = (int)Options.GeneralSettings.FormattingAndTagUsage;
            cbDomain.Items.Clear();
            IDictionary<string, string> domains = new Dictionary<string, string>();
            foreach (string dom in Options.GeneralSettings.Domains) 
            {
                string[] domain = dom.Split('|');
                domains.Add(domain[1], domain[0]);
                cbDomain.Items.Add(domain[0]);
            }
            if (Options.GeneralSettings.SelectedDomain != null)
                cbDomain.SelectedItem = domains[Options.GeneralSettings.SelectedDomain];

        }

        private void localizeContent()
        {
            this.Text = LocalizationHelper.Instance.GetResourceString("OptionsFormCaption");
            this.lblURL.Text = LocalizationHelper.Instance.GetResourceString("URLLabelText");
            this.lblAuth.Text = LocalizationHelper.Instance.GetResourceString("AuthLabelText");
            this.lblSupportedLanguages.Text = LocalizationHelper.Instance.GetResourceString("SupportedLanguagesLabelText");
            this.btnOK.Text = LocalizationHelper.Instance.GetResourceString("OkButtonText");
            this.btnCancel.Text = LocalizationHelper.Instance.GetResourceString("CancelButtonText");
            this.btnHelp.Text = LocalizationHelper.Instance.GetResourceString("HelpButtonText");
            this.lblTagsFormatting.Text = LocalizationHelper.Instance.GetResourceString("TagsAndFormattingLabelText");
            this.cbFormattingTags.Items.Add(LocalizationHelper.Instance.GetResourceString("FormattingAndTags"));     
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
            lbLanguages.Items.Clear();
            
            // do the update in the background
            languageDomainSupport = await updateConfig(tbURL.Text, tbAuth.Text);
            handleUpdateFinished();
        }

        private async Task<LanguageDomainSupport> updateConfig(string url, string auth) {
            var languageDomainSupport = new LanguageDomainSupport()
            {
                URL = url,
                Auth = auth
            };
            try
            {
                // try to get Configuration
                // Do not call any blocking service in the user interface thread; it has to use background threads.
                languageDomainSupport.JSON = await Task.Run(() => TartuNLPServiceHelper.getConfig(url, auth));

                if (languageDomainSupport.JSON == null)
                {
                    //invalid user name or password
                    languageDomainSupport.UpdateSuccessful = false;
                }
                else
                {
                    //successful login
                    languageDomainSupport.UpdateSuccessful = true;

                    //try to get the list of the Domain in the background
                    languageDomainSupport.SupportedDomains = new Dictionary<string, string>();
                    languageDomainSupport.SupportedLanguages = new Dictionary<string, string[]>();
                    foreach (string key in languageDomainSupport.JSON.Keys)
                    {
                        languageDomainSupport.SupportedDomains.Add(languageDomainSupport.JSON[key][0],key);
                        languageDomainSupport.SupportedLanguages.Add(languageDomainSupport.JSON[key][0], languageDomainSupport.JSON[key]);
                    }
                }
}
            catch (Exception ex)
            {
                languageDomainSupport.Exception = ex;
            }

            return languageDomainSupport;
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
                    string caption = LocalizationHelper.Instance.GetResourceString("InvalidURLorAuthCaption");
                    string text = LocalizationHelper.Instance.GetResourceString("InvalidURLorAuthText");
                    MessageBox.Show(this, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    // we have managed to get the supported Domain, display them in the combo box
                    cbDomain.Items.Clear();
                    foreach (string dom in languageDomainSupport.SupportedDomains.Keys)
                    {
                       cbDomain.Items.Add(dom);
                    }
                    cbDomain.SelectedIndex = 0;

                }   
            }
        }

        private void cbDomain_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (languageDomainSupport != null)
            {
                
                lbLanguages.Items.Clear();
                foreach (string language in languageDomainSupport.SupportedLanguages[this.cbDomain.SelectedItem.ToString()])
                {
                    lbLanguages.Items.Add(language);
                }
                lbLanguages.Items.RemoveAt(0);

                btnOK.Enabled = languageDomainSupport.SupportedLanguages[this.cbDomain.SelectedItem.ToString()].Length > 0;
            }
            else 
            {
                lbLanguages.Items.Clear();
                IDictionary<string, string[]> supportedLanguages = new Dictionary<string, string[]>();
                foreach (string languageList in Options.GeneralSettings.SupportedLanguages)
                {
                    string[] languages = languageList.Split('|');
                    supportedLanguages.Add(languages[0], languages[1].Split(','));
                }
                foreach (string language in supportedLanguages[this.cbDomain.SelectedItem.ToString()])
                {
                    lbLanguages.Items.Add(language);
                }
                lbLanguages.Items.RemoveAt(0);

                btnOK.Enabled = supportedLanguages[this.cbDomain.SelectedItem.ToString()].Length > 0;
            }
        }


        private void TartuNLPOptionsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                // if there was a modification, we have to save the changes
                Options.SecureSettings.URL = tbURL.Text;
                Options.SecureSettings.Auth = tbAuth.Text;
                if (languageDomainSupport != null)
                {
                    int counter = 0;
                    string[] supportedLanguages = new string[languageDomainSupport.SupportedLanguages.Count];
                    foreach (KeyValuePair<string, string[]> langs in languageDomainSupport.SupportedLanguages) 
                    {
                        supportedLanguages[counter] = langs.Key + "|" + String.Join(",", langs.Value);
                        counter++;
                    }
                    Options.GeneralSettings.SupportedLanguages = supportedLanguages;
                    string[] supportedDomains = new string[cbDomain.Items.Count];
                    int count = 0;
                    foreach (KeyValuePair<string, string> dom in languageDomainSupport.SupportedDomains)
                    {
                        supportedDomains[count] = dom.Key + "|" + dom.Value;
                        count++;
                    }
                    Options.GeneralSettings.Domains = supportedDomains;
                }
                IDictionary<string,string> domains = new Dictionary<string, string>();
                foreach (string dom in Options.GeneralSettings.Domains)
                {
                    string[] domain = dom.Split('|');
                    domains.Add(domain[0], domain[1]);
                }
                Options.GeneralSettings.SelectedDomain = domains[this.cbDomain.SelectedItem.ToString()];
                Options.GeneralSettings.FormattingAndTagUsage = (FormattingAndTagsUsageOption)cbFormattingTags.SelectedIndex;
                this.lbLanguages.Items.Clear();    
            }
        }
        
        private bool isShowHelpSupported()
        {
            // If the resource is remote and downloaded to an old client, it can happen that the client does not support ShowHelp yet.            
            return environment.GetType().GetInterface(nameof(IEnvironment2)) != null;
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            (environment as IEnvironment2)?.ShowHelp("googlemt-settings.html");
        }

        
    }
}
