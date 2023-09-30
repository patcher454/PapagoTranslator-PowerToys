using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Community.PowerToys.Run.Plugin.PapagoTranslator.Enums;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Job;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Models;

using ManagedCommon;

using Microsoft.PowerToys.Settings.UI.Library;

using Wox.Infrastructure.Storage;
using Wox.Plugin;

using static Microsoft.PowerToys.Settings.UI.Library.PluginAdditionalOption;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator
{
    public class Main : IPlugin, IPluginI18n, ISavable, IContextMenu, IDisposable, ISettingProvider
    {
        private const string APIClientId = nameof(APIClientId);
        private const string APISecretKey = nameof(APISecretKey);

        private static readonly PluginJsonStorage<PapagoTranslatorSetting> _storage = new PluginJsonStorage<PapagoTranslatorSetting>();
        private static readonly PapagoTranslatorSetting _settings = _storage.Load();

        private static string iconPath;

        public string Name => Properties.Resources.plugin_name;

        public string Description => Properties.Resources.plugin_description;


        private bool _disposed;
        private PluginInitContext _context;

        public void Init(PluginInitContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(paramName: nameof(context));
            }

            this._context = context;
            this._context.API.ThemeChanged += OnThemeChanged;
            UpdateIconPath(this._context.API.GetCurrentTheme());
        }

        public IEnumerable<PluginAdditionalOption> AdditionalOptions => new List<PluginAdditionalOption>()
        {
            new PluginAdditionalOption()
            {
                TextValue = _settings.ClientID,
                PluginOptionType = AdditionalOptionType.Textbox,
                Key = APIClientId,
                DisplayLabel = "ClientId",
                DisplayDescription = Properties.Resources.clientId_description,
            },

            new PluginAdditionalOption()
            {
                TextValue = _settings.ClientSecret,
                PluginOptionType = AdditionalOptionType.Textbox,
                Key = APISecretKey,
                DisplayLabel = "Client Secret",
                DisplayDescription = Properties.Resources.secret_description,
            },
        };

        private static void UpdateIconPath(Theme theme)
        {
            if (theme == Theme.Light || theme == Theme.HighContrastWhite)
            {
                iconPath = "Images/Papago_Logo.png";
            }
            else
            {
                iconPath = "Images/Papago_Logo.png";
            }
        }

        private static void OnThemeChanged(Theme oldTheme, Theme newTheme)
        {
            UpdateIconPath(newTheme);
        }

        private ContextMenuResult CreateContextMenuEntry(ResponseTranslation result)
        {
            return new ContextMenuResult
            {
                PluginName = this.Name,
                Title = Properties.Resources.context_menu_copy,
                Glyph = "\xE8C8",
                FontFamily = "Segoe MDL2 Assets",
                AcceleratorKey = Key.Enter,
                Action = _ =>
                {
                    bool ret = false;
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            Clipboard.SetText(result.Message.Result.TranslatedText);
                            ret = true;
                        }
                        catch (ExternalException)
                        { }
                    });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    return ret;
                },
            };
        }

        public List<ContextMenuResult> LoadContextMenus(Result selectedResult)
        {
            if (!(selectedResult?.ContextData is ResponseTranslation))
            {
                return new List<ContextMenuResult>();
            }

            List<ContextMenuResult> contextResults = new List<ContextMenuResult>();
            ResponseTranslation result = selectedResult.ContextData as ResponseTranslation;
            contextResults.Add(this.CreateContextMenuEntry(result));

            return contextResults;
        }

        public List<Result> Query(Query query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(paramName: nameof(query));
            }

            ConvertModel convertModel;

            try
            {
                convertModel = InputInterpreter.Parse(query, _settings);
            }
            catch (Exception)
            {
                return new List<Result>();
            }

            if (convertModel == null)
            {
                return new List<Result>();
            }

            return TranslationHandler.Convert(convertModel, _settings)
                .Select(GetResult)
                .ToList();
        }

        private Result GetResult(ResponseTranslation result)
        {
            if (_settings.ClientID == string.Empty || _settings.ClientSecret == string.Empty)
            {
                return new Result
                {
                    Title = Properties.Resources.invalid_clientId_secretKey,
                    IcoPath = iconPath,
                    Score = 300,
                    SubTitle = string.Empty,
                };
            }

            return new Result
            {
                ContextData = result,
                Title = $"{result.Message.Result.SrcLangType} -> {result.Message.Result.TarLangType} : {result.Message.Result.TranslatedText}",
                IcoPath = iconPath,
                Score = 300,
                SubTitle = string.Format(Properties.Resources.copy_to_clipboard, result.Message.Result.TarLangType),
                Action = c =>
                {
                    var ret = false;
                    var thread = new Thread(() =>
                    {
                        try
                        {
                            Clipboard.SetText(result.Message.Result.TranslatedText);
                            ret = true;
                        }
                        catch (ExternalException e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    });
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    return ret;
                },
            };
        }

        public void UpdateSettings(PowerLauncherPluginSettings settings)
        {
            if (settings.AdditionalOptions != null)
            {
                var clientId = settings.AdditionalOptions.FirstOrDefault(x => x.Key == APIClientId)?.TextValue ?? string.Empty;
                var secretKey = settings.AdditionalOptions.FirstOrDefault(x => x.Key == APISecretKey)?.TextValue ?? string.Empty;

                _settings.ClientID = clientId;
                _settings.ClientSecret = secretKey;
            }
        }

        public string GetTranslatedPluginTitle()
        {
            return Properties.Resources.plugin_name;
        }

        public string GetTranslatedPluginDescription()
        {
            return Properties.Resources.plugin_description;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_context != null && _context.API != null)
                    {
                        _context.API.ThemeChanged -= OnThemeChanged;
                    }

                    _disposed = true;
                }
            }
        }

        public Control CreateSettingPanel()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _storage.Save();
        }
    }
}
