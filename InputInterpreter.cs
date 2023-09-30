using System;
using System.Collections.Generic;
using System.Text;

using Community.PowerToys.Run.Plugin.PapagoTranslator.Enums;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Job;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Models;

using Wox.Plugin;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator
{
    internal class InputInterpreter
    {
        private static string[] SplitSearch(Query query)
        {
            int secondSearchStartIndex = query.Search.IndexOf(' ');
            return new string[]
            {
                query.Search.Substring(0, secondSearchStartIndex),
                query.Search.Substring(secondSearchStartIndex, query.Search.Length - secondSearchStartIndex),
            };
        }

        public static ConvertModel Parse(Query query, PapagoTranslatorSetting settings)
        {
            var splited = SplitSearch(query);

            if (splited.Length == 2)
            {
                if (!string.IsNullOrEmpty(splited[0]) && !string.IsNullOrEmpty(splited[1]))
                {
                    var target = LangCodeEnums.Parse(splited[0]);

                    if (target != LangCodeEnums.Code.unk)
                    {
                        try
                        {
                            var source = LangCodeEnums.Parse(Job_Http.DetectLang(splited[1], settings.ClientID, settings.ClientSecret).GetAwaiter().GetResult());
                            if (source != LangCodeEnums.Code.unk)
                            {
                                return new ConvertModel(source, target, splited[1]);
                            }
                        }
                        catch (Exception)
                        {
                            return null;
                        }
                    }
                }
            }

            return null;
        }
    }
}
