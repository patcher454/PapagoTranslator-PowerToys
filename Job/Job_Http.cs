using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

using Community.PowerToys.Run.Plugin.PapagoTranslator.Enums;
using Community.PowerToys.Run.Plugin.PapagoTranslator.Models;

namespace Community.PowerToys.Run.Plugin.PapagoTranslator.Job
{
    internal class Job_Http
    {
        private static HttpClient httpClient;

        private static string oldClientId;

        private static string oldSecretKey;

        private static void Init(string clientId, string secretKey)
        {
            oldClientId = clientId;
            oldSecretKey = secretKey;

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://openapi.naver.com/v1/papago/");
            httpClient.DefaultRequestHeaders.Add("X-Naver-Client-Id", clientId);
            httpClient.DefaultRequestHeaders.Add("X-Naver-Client-Secret", secretKey);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<string> DetectLang(string text, string clientId, string secretKey)
        {
            if (httpClient == null || oldClientId != clientId || oldSecretKey != secretKey)
            {
                Init(clientId, secretKey);
            }

            var formContent = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("query", text),
            });

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync("detectLangs", formContent);
                return JsonSerializer.Deserialize<ResponseDetectLangs>(await response.Content.ReadAsStringAsync()).LangCode;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static async Task<ResponseTranslation> Translation(ConvertModel convertModel, string clientId, string secretKey)
        {
            if (httpClient == null || oldClientId != clientId || oldSecretKey != secretKey)
            {
                Init(clientId, secretKey);
            }

            var formContent = new FormUrlEncodedContent(new[]
            {
                 new KeyValuePair<string, string>("source", LangCodeEnums.ToString(convertModel.SourceLang)),
                 new KeyValuePair<string, string>("target", LangCodeEnums.ToString(convertModel.Target)),
                 new KeyValuePair<string, string>("text", convertModel.Text),
            });

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync("n2mt", formContent);
                return JsonSerializer.Deserialize<ResponseTranslation>(await response.Content.ReadAsStringAsync());
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
