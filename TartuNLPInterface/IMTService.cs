﻿using System.Collections.Generic;
using System.ServiceModel;

namespace TartuNLPInterface
{
    [ServiceContract]
    public interface IMTService
    {
        [OperationContract]
        string Login(string userName, string password);

        [OperationContract]
        List<string> ListSupportedLanguages(string tokenCode);

        [OperationContract]
        string Translate(string tokenCode, string input, string srcLangCode, string trgLangCode);

        [OperationContract]
        List<string> BatchTranslate(string tokenCode, List<string> input, string srcLangCode, string trgLangCode);

        [OperationContract]
        void StoreTranslation(string tokenCode, string source, string target, string srcLangCode, string trgLangCode);

        [OperationContract]
        int[] BatchStoreTranslation(string tokenCode, List<string> sources, List<string> targets, string srcLangCode, string trgLangCode);
    }
}
