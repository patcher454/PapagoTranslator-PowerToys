using Community.PowerToys.Run.Plugin.PapagoTranslator.Enums;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator.Models
{
    internal class ConvertModel
    {
        public LangCodeEnums.Code SourceLang { get; }

        public LangCodeEnums.Code Target { get; }

        public string Text { get; }

        public ConvertModel(LangCodeEnums.Code sourceLang, LangCodeEnums.Code target, string text)
        {
            this.SourceLang = sourceLang;
            this.Target = target;
            this.Text = text;
        }
    }
}
