using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using MemoQ.Addins.Common.DataStructures;
using MemoQ.Addins.Common.Utils;
using MemoQ.MTInterfaces;

namespace TartuNLP
{
    /// <summary>
    /// Session that perform actual translation or storing translations. Created on a segment-by-segment basis, or once for batch operations.
    /// </summary>
    /// <remarks>
    /// Implementation checklist:
    ///     - The MTException class is used to wrap the original exceptions occurred during the translation.
    ///     - All allocated resources are disposed correctly in the session.
    /// </remarks>
    public class TartuNLPSession : ISession, ISessionForStoringTranslations
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
        /// Options of the plugin.
        /// </summary>
        private readonly TartuNLPOptions options;

        public TartuNLPSession(string srcLangCode, string trgLangCode, TartuNLPOptions options)
        {
            this.srcLangCode = srcLangCode;
            this.trgLangCode = trgLangCode;
            this.options = options;
        }

        #region ISession Members

        /// <summary>
        /// Translates a single segment, possibly using a fuzzy TM hit for improvement
        /// </summary>
        public TranslationResult TranslateCorrectSegment(Segment segment, Segment tmSource, Segment tmTarget)
        {
            var result = new TranslationResult();

            try
            {
                var textsToTranslate = new List<string>(0)
                {
                    createTextFromSegment(segment, options.GeneralSettings.FormattingAndTagUsage)
                };
                var translation = TartuNLPServiceHelper.Translate(options, textsToTranslate, srcLangCode, trgLangCode)[0];
                result.Translation = createSegmentFromResult(segment, translation, options.GeneralSettings.FormattingAndTagUsage);
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                var localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
                result.Exception = new MTException(string.Format(localizedMessage, e.Message),
                    $"A network error occured ({e.Message}).", e);
            }

            return result;
        }

        /// <summary>
        /// Translates multiple segments, possibly using a fuzzy TM hit for improvement
        /// </summary>
        public TranslationResult[] TranslateCorrectSegment(Segment[] segments, Segment[] tmSources, Segment[] tmTargets)
        {
            var results = new TranslationResult[segments.Length];
            for (var i = 0; i < segments.Length; i++)
            {
                results[i] = new TranslationResult();
            }

            try
            {
                var texts = segments.Select(s => createTextFromSegment(s, options.GeneralSettings.FormattingAndTagUsage)).ToList();
                var i = 0;
                foreach (var translation in TartuNLPServiceHelper.Translate(options, texts, srcLangCode, trgLangCode))
                {
                    results[i].Translation = createSegmentFromResult(segments[i], translation, options.GeneralSettings.FormattingAndTagUsage);
                    i++;
                }
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                foreach (var result in results)
                {
                    result.Exception = new MTException(e.Message, e.Message, e);
                }
            }

            return results;
        }

        /// <summary>
        /// Creates the text to translate from the segment according to the settings. Appends tags and formatting if needed.
        /// </summary>
        private static string createTextFromSegment(Segment segment, 
            FormattingAndTagsUsageOption formattingAndTagOption)
        {
            var text = WebUtility.HtmlDecode(SegmentXMLConverter.ConvertSegment2Xml(segment, true));
            text = Regex.Replace(text, "<spec_char val=\"<\"/>", "<spec_char val=\"&lt;\"/>");
            text = Regex.Replace(text, "<spec_char val=\">\"/>", "<spec_char val=\"&gt;\"/>");
            return text;
        }

        private static Segment createSegmentFromResult(Segment originalSegment, string translatedText, 
            FormattingAndTagsUsageOption formattingAndTagUsage)
        {
            return SegmentXMLConverter.ConvertXML2Segment(translatedText, originalSegment.ITags);
        }

        #endregion

        #region ISessionForStoringTranslations

        public void StoreTranslation(TranslationUnit transunit)
        {
            
        }

        public int[] StoreTranslation(TranslationUnit[] transunits)
        {
            return new int[0];
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            // dispose your resources if needed
        }

        #endregion
    }
}
