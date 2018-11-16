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

        [LuisIntent("AllCafes")]
        public async Task ShowCafe(IDialogContext context, LuisResult result)
        {
            foreach (var searchEntity in result.Entities)
            {
                if (searchEntity.Entity.ToLower().CompareTo("mekstar") == 0){
                    await new ShowHeroCard().showCafeHeroCard(context, 1);
                }
                else if(searchEntity.Entity.ToLower().CompareTo("umut") == 0 || searchEntity.Entity.ToLower().CompareTo("hope") == 0)
                {
                    await new ShowHeroCard().showCafeHeroCard(context, 2);
                }
                else if (searchEntity.Entity.ToLower().CompareTo("mahur") == 0)
                {
                    await new ShowHeroCard().showCafeHeroCard(context, 3);
                }
                else
                {
                    await new ShowHeroCard().showCafeHeroCard(context, 4);
                }
            }
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("MonthlyList")]
        public async Task MonthlyListIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(24));
        }
        [LuisIntent("DailyList")]
        public async Task DailyListIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync($"Bugünün menüsü: " + new ChatbotDataBase().getTodayMailList());
        }
        [LuisIntent("CafeteriaHours")]
        public async Task CafeteriaHoursIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(25));
        }
        [LuisIntent("AcademicCalendar")]
        public async Task AcademicCalendar(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(1));
        }
        [LuisIntent("StudentId")]
        public async Task StundentId(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(2));
        }
        [LuisIntent("CreditCard")]
        public async Task CreditCard(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(3));
        }
        [LuisIntent("Library")]
        public async Task Library(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(4));
        }
        [LuisIntent("Midterm")]
        public async Task Midterm(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheExams(9));
        }
        [LuisIntent("EsdUsd")]
        public async Task EsdUsd(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheExams(13));
        }
        [LuisIntent("ExcuseExam")]
        public async Task ExcuseExam(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheExams(11));
        }
        [LuisIntent("MakeupExam")]
        public async Task MakeupExam(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheExams(14));
        }
        [LuisIntent("FinalExam")]
        public async Task FinalExam(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheExams(12));
        }
        [LuisIntent("FacultyOfEducation")]
        public async Task FacultyOfEducation(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Eðitim Fakültesi", 13);

            //await this.ShowLuisResult(context, result);
        }
        [LuisIntent("FacultyOfArchitectureAndDesign")]
        public async Task FacultyOfArchitectureAndDesign(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Mimarlýk ve Tasarým Fakültesi", 27);

        }
        [LuisIntent("FacultyOfAviationAndSpaceSciences")]
        public async Task FacultyOfAviationAndSpaceSciences(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Havacýlýk ve Uzay Fakültesi", 26);

        }
        [LuisIntent("FacultyOfDentistry")]
        public async Task FacultyOfDentistry(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Diþ Hekimliði Fakültesi", 24);

        }
        [LuisIntent("FacultyOfCommunication")]
        public async Task FacultyOfCommunication(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Ýletiþim Fakültesi", 18);

        }
        [LuisIntent("FacultyOfEconomicsAndAdministrativeSciences")]
        public async Task FacultyOfEconomicsAndAdministrativeSciences(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Ýktisadi ve Ýdari Bilimler Fakültesi", 16);

        }
        [LuisIntent("FacultyOfFineArts")]
        public async Task FacultyOfFineArts(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Güzel Sanatlar Fakültesi", 25);

        }
        [LuisIntent("FacultyOfHealthSciences")]
        public async Task FacultyOfHealthSciences(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Saðlýk Bilimleri Fakültesi", 20);

        }
        [LuisIntent("FacultyOfLaw")]
        public async Task FacultyOfLaw(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Hukuk Fakültesi", 10);

        }
        [LuisIntent("FacultyOfMedicine")]
        public async Task FacultyOfMedicine(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Týp Fakültesi", 19);

        }
        [LuisIntent("FacultyOfSportsSciences")]
        public async Task FacultyOfSportsSciences(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Spor Bilimleri Fakültesi", 22);

        }
        [LuisIntent("FacultyOfTechnology")]
        public async Task FacultyOfTechnology(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Teknoloji Fakültesi", 17);

        }
        [LuisIntent("FacultyOfTheology")]
        public async Task FacultyOfTheology(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Ýlahiyat Fakültesi", 21);

        }
        [LuisIntent("HighSchoolOfForeignLanguages")]
        public async Task HighSchoolOfForeignLanguages(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Yabancý Diller Yüksek Okulu", 23);

        }
        [LuisIntent("FacultyOfEngineeringB")]
        public async Task FacultyOfEngineeringB(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Mühendislik Fakültesi B", 15);

        }
        [LuisIntent("FacultyOfEngineeringA")]
        public async Task FacultyOfEngineeringA(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Mühendislik Fakültesi A", 14);

        }
        [LuisIntent("FacultyofArtsandSciencesB")]
        public async Task FacultyofArtsandSciencesB(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Fen Edebiyat Fakültesi B", 12);

        }
        [LuisIntent("FacultyofArtsandSciencesA")]
        public async Task FacultyofArtsandSciencesA(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Fen Edebiyat Fakültesi A", 11);

        }
        [LuisIntent("KentKart")]
        public async Task KentKart(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Kent Kart", 36);

        }
        [LuisIntent("LibraryLocation")]
        public async Task LibraryLocation(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Kütüphane", 29);

        }
        [LuisIntent("Mosque")]
        public async Task Mosque(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Mosque", 31);

        }
        [LuisIntent("Masjid")]
        public async Task Masjid(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Masjid", 30);

        }
        [LuisIntent("KouStore")]
        public async Task KouStore(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "KouStore ", 28);

        }
        [LuisIntent("IsBankasi")]
        public async Task IsBankasi(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "Is Bankasi", 1);

        }
        [LuisIntent("CafeteriaLocation")]
        public async Task CafeteriaLocation(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "CafeteriaLocation", 32);

        }
        [LuisIntent("StudentAffairs")]
        public async Task StudentAffairs(IDialogContext context, LuisResult result)
        {
            await new ShowHeroCard().showHeroCard(context, "StudentAffairs", 37);

        }
        [LuisIntent("StudentClub")]
        public async Task StudentClub(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(23));

        }
        [LuisIntent("CashPoint")]
        public async Task CashPoint(IDialogContext context, LuisResult result)
        {
            for (int i = 12; i < 20; i++)
            {
                await context.PostAsync(new ChatbotDataBase().GetQuestions(i)+"\n");
            }
        }
        [LuisIntent("Pharmacy")]
        public async Task Pharmacy(IDialogContext context, LuisResult result)
        {
            for (int i = 20; i < 23; i++)
            {
                await context.PostAsync(new ChatbotDataBase().GetQuestions(i) + "\n");
            }
        }
        [LuisIntent("Transcript")]
        public async Task Transcript(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(10));

        }
        [LuisIntent("StudentCertificate")]
        public async Task StudentCertificate(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(8));

        }
        [LuisIntent("Buses")]
        public async Task Buses(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(7));

        }
        [LuisIntent("Mediko")]
        public async Task Mediko(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(11));

        }
        [LuisIntent("Cafes")]
        public async Task Cafes(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(5));

            //await this.ShowLuisResult(context, result);
        }
        [LuisIntent("Hello")]
        public async Task Hello(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetQuestions(6));

        }
        [LuisIntent("MekstarCafe")]
        public async Task MekstarCafe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheCafes(1));

  
        }
        [LuisIntent("UmutCafe")]
        public async Task UmutCafe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheCafes(2));

        }

        [LuisIntent("MahurCafe")]
        public async Task MahurCafe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheCafes(3));

        }

        [LuisIntent("DeepCafe")]
        public async Task DeepCafe(IDialogContext context, LuisResult result)
        {
            await context.PostAsync(new ChatbotDataBase().GetTheCafes(4));

        }
        /*   [LuisIntent("Location")]
           public async Task LocationIntent(IDialogContext context, LuisResult result)
           {
               // databasede konum google map linki ve image linki tutulsun
               await new ShowHeroCard().showHeroCard(context, "Eðitim Fakültesi", 13);

               await this.ShowLuisResult(context, result);
           }*/

        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}