using System;
using System.Drawing;
using System.Reflection;
using MemoQ.MTInterfaces;

namespace TartuNLP
{
    /// <summary>
    /// MT engine for a particular language combination.
    /// </summary>
    /// <remarks>
    /// Implementation checklist:
    ///     - The MTException class is used to wrap the original exceptions occurred during the translation.
    ///     - All allocated resources are disposed correctly in the session.
    /// </remarks>
    public class TartuNLPEngine : EngineBase
    {
        /// <summary>
        /// The source language.
        /// </summary>
        private readonly string srcLangCode;

        /// <summary>
        /// The target language.
        /// </summary>
        private readonly string trgLangCode;

        /// <summary>
        /// Plugin options
        /// </summary>
        private readonly TartuNLPOptions options;

        public TartuNLPEngine(string srcLangCode, string trgLangCode, TartuNLPOptions options)
        {
            this.srcLangCode = srcLangCode;
            this.trgLangCode = trgLangCode;
            this.options = options;
        }

        #region IEngine Members

        /// <summary>
        /// Creates a session for translating segments. Session will not be used in a multi-threaded way.
        /// </summary>
        public override ISession CreateLookupSession()
        {
            return new TartuNLPSession(srcLangCode, trgLangCode, options);
        }

        /// <summary>
        /// Set an engine-specific custom property, e.g., subject matter area.
        /// </summary>
        public override void SetProperty(string name, string value)
        {
        }

        /// <summary>
        /// Returns a small icon to be displayed under translation results when an MT hit is selected from this plugin.
        /// </summary>
        public override Image SmallIcon =>
            Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("TartuNLP.TartuNLP.png") ??
                             throw new InvalidOperationException());

        /// <summary>
        /// Indicates whether the engine supports the adjustment of fuzzy TM hits through machine translation.
        /// </summary>
        public override bool SupportsFuzzyCorrection => false;

        /// <summary>
        /// Creates a session for translating segments. Session will not be used in a multi-threaded way.
        /// </summary>
        public override ISessionForStoringTranslations CreateStoreTranslationSession()
        {
            return new TartuNLPSession(srcLangCode, trgLangCode, options);
        }

        #endregion

        #region IDisposable Members

        public override void Dispose()
        {
            // dispose your resources if needed
        }

        #endregion
    }
}