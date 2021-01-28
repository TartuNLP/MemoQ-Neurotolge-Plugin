using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MemoQ.Addins.Common.Framework;
using MemoQ.MTInterfaces;

namespace TartuNLP
{
    /// <summary>
    /// The main class of the MT plugin.
    /// </summary>
    public class TartuNLPPluginDirector : PluginDirectorBase, IModule
    {
        /// <summary>
        /// The memoQ's application environment; e.g., to provide UI language settings etc. to the plugin.
        /// </summary>
        private IEnvironment environment;

        #region IModule Members

        public void Cleanup()
        {
            // write your cleanup code here
        }

        public void Initialize(IModuleEnvironment env)
        {
            // write your initialization code here
        }

        public bool IsActivated => true;

        #endregion

        #region IPluginDirector Members

        /// <summary>
        /// Indicates whether interactive lookup (in the translation grid) is supported or not.
        /// </summary>
        public override bool InteractiveSupported => true;

        /// <summary>
        /// Indicates whether batch lookup is supported or not.
        /// </summary>
        public override bool BatchSupported => true;

        /// <summary>
        /// Indicates whether storing translations is supported.
        /// </summary>
        public override bool StoringTranslationSupported => false;

        /// <summary>
        /// The plugin's non-localized name.
        /// </summary>
        public override string PluginID => "TartuNLP";

        /// <summary>
        /// Returns the friendly name to show in memoQ's Tools / Options.
        /// </summary>
        public override string FriendlyName => "TartuNLP";

        /// <summary>
        /// Return the copyright text to show in memoQ's Tools / Options.
        /// </summary>
        public override string CopyrightText => "(C) University of Tartu";

        /// <summary>
        /// Return a 48x48 display icon to show in MemoQ's Tools / Options. Black is the transparent color.
        /// </summary>
        public override Image DisplayIcon => Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("TartuNLP.TartuNLP.png") ?? throw new InvalidOperationException());

        /// <summary>
        /// The memoQ's application environment; e.g., to provide UI language settings etc. to the plugin.
        /// </summary>
        public override IEnvironment Environment
        {
            set
            {
                environment = value;

                // initialize the localization helper
                LocalizationHelper.Instance.SetEnvironment(value);
            }
        }

        /// <summary>
        /// Tells memoQ if the plugin supports the provided language combination. The strings provided are memoQ language codes.
        /// </summary>
        public override bool IsLanguagePairSupported(LanguagePairSupportedParams args)
        {
            var options = new TartuNLPOptions(args.PluginSettings); 
            return options.GeneralSettings.SupportedLanguages.Contains((args.SourceLangCode, args.TargetLangCode));
        }

        /// <summary>
        /// Creates an MT engine for the supplied language pair.
        /// </summary>
        public override IEngine2 CreateEngine(CreateEngineParams args)
        {
            return new TartuNLPEngine(args.SourceLangCode, args.TargetLangCode, new TartuNLPOptions(args.PluginSettings));
        }

        /// <summary>
        /// Shows the plugin's options form.
        /// </summary>
        public override PluginSettings EditOptions(IWin32Window parentForm, PluginSettings settings)
        {
            using (var form = new TartuNLPOptionsForm(environment) { Options = new TartuNLPOptions(settings) })
            {
                if (form.ShowDialog(parentForm) == DialogResult.OK)
                {
                    environment.PluginAvailabilityChanged();
                }
                return form.Options.GetSerializedSettings();
            }
        }

        #endregion
    }
}
