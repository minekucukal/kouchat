using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace LuisBot.Dialogs
{
    [Serializable]
    public class Translator: IDialog<object>
    {
        public const string key = "ee18c9de85cf4bf6bd2062d9a98cb954";
        public const string baseUrl = @"http://api.microsofttranslator.com/v2/http.svc/translate?text={0}&from={1}&to={2}&category=general";

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            string text = ((await result) as Activity).Text;
            string translation = await TranslateTextAsync(key, text);
            await context.PostAsync(translation);
        }

        public async Task<string> TranslateTextAsync(string key, string text)
        {
            string url = string.Format(baseUrl, text, "tr", "en");
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                var response = await client.GetAsync(url);
                var xml = await response.Content.ReadAsStringAsync();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                return doc.InnerText;
            }
        }
    }
}