using System;
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
        public TranslationResult TranslateCorrectSegment(Segment segm, Segment tmSource, Segment tmTarget)
        {
            TranslationResult result = new TranslationResult();

            try
            {
                string textToTranslate = createTextFromSegment(segm, options.GeneralSettings.FormattingAndTagUsage);
                string translation = TartuNLPServiceHelper.Translate(options, textToTranslate, this.srcLangCode, this.trgLangCode);
                result.Translation = createSegmentFromResult(segm, translation, options.GeneralSettings.FormattingAndTagUsage);
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
                result.Exception = new MTException(string.Format(localizedMessage, e.Message), string.Format("A network error occured ({0}).", e.Message), e);
            }

            return result;
        }

        /// <summary>
        /// Translates multiple segments, possibly using a fuzzy TM hit for improvement
        /// </summary>
        public TranslationResult[] TranslateCorrectSegment(Segment[] segs, Segment[] tmSources, Segment[] tmTargets)
        {
            TranslationResult[] results = new TranslationResult[segs.Length];
            for (int i = 0; i < segs.Length; i++)
            {
                results[i] = new TranslationResult();
            }

            try
            {
                var texts = segs.Select(s => createTextFromSegment(s, options.GeneralSettings.FormattingAndTagUsage)).ToList();
                int i = 0;
                foreach (string translation in TartuNLPServiceHelper.BatchTranslate(options, texts, this.srcLangCode, this.trgLangCode))
                {
                    results[i].Translation = createSegmentFromResult(segs[i], translation, options.GeneralSettings.FormattingAndTagUsage);
                    i++;
                }
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                foreach (TranslationResult result in results)
                {
                    result.Exception = new MTException(e.Message, e.Message, e);
                }
            }

            return results;
        }

        /// <summary>
        /// Creates the text to translate from the segment according to the settings. Appends tags and formatting if needed.
        /// </summary>
        private string createTextFromSegment(Segment segment, FormattingAndTagsUsageOption formattingAndTagOption)
        {
            var text = WebUtility.HtmlDecode(SegmentXMLConverter.ConvertSegment2Xml(segment, true));
            text = Regex.Replace(text, "<spec_char val=\"<\"/>", "<spec_char val=\"&lt;\"/>");
            text = Regex.Replace(text, "<spec_char val=\">\"/>", "<spec_char val=\"&gt;\"/>");
            return text;
        }

        private static Segment createSegmentFromResult(Segment originalSegment, string translatedText, FormattingAndTagsUsageOption formattingAndTagUsage)
        {
            return SegmentXMLConverter.ConvertXML2Segment(translatedText, originalSegment.ITags);
        }

        #endregion

        #region ISessionForStoringTranslations

        public void StoreTranslation(TranslationUnit transunit)
        {
            try
            {
                TartuNLPServiceHelper.StoreTranslation(options, transunit.Source.PlainText, transunit.Target.PlainText, this.srcLangCode, this.trgLangCode);
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
                throw new MTException(string.Format(localizedMessage, e.Message), string.Format("A network error occured ({0}).", e.Message), e);
            }
        }

        public int[] StoreTranslation(TranslationUnit[] transunits)
        {

            try
            {
                return TartuNLPServiceHelper.BatchStoreTranslation(options,
                                        transunits.Select(s => s.Source.PlainText).ToList(), transunits.Select(s => s.Target.PlainText).ToList(),
                                        this.srcLangCode, this.trgLangCode);
            }
            catch (Exception e)
            {
                // Use the MTException class is to wrap the original exceptions occurred during the translation.
                string localizedMessage = LocalizationHelper.Instance.GetResourceString("NetworkError");
                throw new MTException(string.Format(localizedMessage, e.Message), string.Format("A network error occured ({0}).", e.Message), e);
            }
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
