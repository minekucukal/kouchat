using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace LuisBot.Dialogs
{
    public class ShowHeroCard
    {
        private static Attachment GetHeroCard(string locationName, int index)
        {
            var title = new ChatbotDataBase().GetTheLocationTitle(index);
            var imageLink = new ChatbotDataBase().GetTheLocationImageLink(index);
            var mapLink = new ChatbotDataBase().GetTheLocationMapLink(index);
            var heroCard = new HeroCard
            {
                Title = title,
                Images = new List<CardImage> { new CardImage(imageLink) },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Haritada Aç", value: mapLink) }
            };

            return heroCard.ToAttachment();
        }

        public async virtual Task showHeroCard(IDialogContext context, string locationName, int index)
        {
            var message = context.MakeMessage();
            var attachment = GetHeroCard(locationName,index);
            message.Attachments.Add(attachment);
            await context.PostAsync(message);
        }






        private static Attachment GetCafeHeroCard(int index)
        {
            var title = new ChatbotDataBase().GetTheCafeInfo(index,1);
            var text = new ChatbotDataBase().GetTheCafeInfo(index, 2);
            var mapLink = new ChatbotDataBase().GetTheCafeInfo(index, 3);
            var heroCard = new HeroCard
            {
                Title = title,
                Text = text,
                Images = new List<CardImage> { new CardImage("https://preview.ibb.co/jiiGqd/Edinburgh_University_Levels_02.jpg") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Haritada Aç", value: mapLink) }
            };

            return heroCard.ToAttachment();
        }



        public async virtual Task showCafeHeroCard(IDialogContext context,int index)
        {
            var message = context.MakeMessage();
            var attachment = GetCafeHeroCard(index);
            message.Attachments.Add(attachment);
            await context.PostAsync(message);
        }
    }
}