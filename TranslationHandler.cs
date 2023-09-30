using System;
using System.Collections.Generic;

using Community.PowerToys.Run.Plugin.PapagoTranslator.Job;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Models;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator
{
    internal class TranslationHandler
    {
        public static IEnumerable<ResponseTranslation> Convert(ConvertModel convertModel, PapagoTranslatorSetting settings)
        {
            var results = new List<ResponseTranslation>();
            try
            {
                results.Add(Job_Http.Translation(convertModel, settings.ClientID, settings.ClientSecret).GetAwaiter().GetResult());
            }
            catch (Exception)
            {
                return new List<ResponseTranslation>();
            }

            return results;
        }
    }
}
