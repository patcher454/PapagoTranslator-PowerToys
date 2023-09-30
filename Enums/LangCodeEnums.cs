using System;
using System.Collections.Generic;
using System.Text;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator.Enums
{
    internal static class LangCodeEnums
    {
        public enum Code
        {
            ko,     // Korean
            en,     // English
            ja,     // Japanese
            zh_CN,  // Simplified Chinese
            zh_TW,  // Traditional Chinese
            vi,     // Vietnamese
            id,     // Indonesian
            th,     // Thai
            de,     // German
            ru,     // Russian
            es,     // Spanish
            it,     // Italian
            fr,     // French
            unk,    // Unknow
        }

        public static Code Parse(string codeString)
        {
            switch (codeString)
            {
                case "cn":
                case "zh-CN":
                case "zh_CN":
                    return Code.zh_CN;
                case "tw":
                case "zh-TW":
                case "zh_TW":
                    return Code.zh_TW;
                default:
                    try
                    {
                        return Enum.Parse<Code>(codeString.ToLowerInvariant());
                    }
                    catch (Exception)
                    {
                        return Code.unk;
                    }
            }
        }

        public static string ToString(Code code)
        {
            switch (code)
            {
                case Code.zh_CN:
                    return "zh-CN";
                case Code.zh_TW:
                    return "zh-TW";
                default:
                    return code.ToString();
            }
        }
    }
}
