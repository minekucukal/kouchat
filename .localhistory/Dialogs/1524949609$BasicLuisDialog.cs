using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using LuisBot.Dialogs;

namespace Microsoft.Bot.Sample.LuisBot
{
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisDialog<object>
    {
        public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }

        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            string message = $"Üzgünüm, '{result.Query}' içeriðini tam olarak anlayamadým. Size nasýl yardýmcý olacaðýmý 'help' veya 'help me' yazarak öðrenebilirsiniz..";
            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("MonthlyList")]
        public async Task MonthlyListIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }
        [LuisIntent("DailyList")]
        public async Task DailyListIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Bu günün menüsü: " + new ChatbotDataBase().getTodayMailList());
            await this.ShowLuisResult(context, result);
        }
        [LuisIntent("CafeteriaHours")]
        public async Task CafeteriaHoursIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }
        [LuisIntent("Location")]
        public async Task LocationIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}