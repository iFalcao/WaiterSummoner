using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WaiterSummoner.Services
{
    public class SummonNameGeneratorService
    {
        #region Props
        private readonly HashSet<string> AVAILABLE_SUMMON_NAMES = new HashSet<string>
        {
            "Consagrado",
            "Abençoado",
            "Entidade",
            "Feiticeiro",
            "Imperador",
            "Diplomata",
            "Embaixador",
            "Almirante",
            "Comendador",
            "Bacharel",
            "Chanceler",
            "Engenheiro",
            "Eclesiasta",
            "Paladino",
            "Engenheiro",
            "Estivador",
            "Menestrel",
            "Suserano",
            "Leviatã",
            "Cangaceiro",
            "Sambarilove",
            "Nórdico",
            "Vigilante",
            "Combatente",
            "Batizado",
            "Aspirante",
            "Companheiro",
            "Estrategista",
            "Misterioso",
            "Acionista",
            "Campeão",
            "Patrão",
            "Camponês",
            "Peregrino",
            "Comediante",
            "Garanhão",
            "Maestro",
            "Supra-Sumo",
            "Influencer",
            "Sensei",
            "Piloto",
            "Inoxidável",
            "Palestrinha",
            "Extravagante",
            "Monarca",
            "Mascarado",
            "Peregrino",
            "Templário",
            "Faraó",
            "Emissário",
            "Faraó",
            "Xerife",
            "Protagonista",
            "Benevolente",
            "Afortunado",
            "Avassalador",
            "Homologado",
            "Capitão",
            "Talentoso",
            "Comandante",
            "Iluminado",
            "Camarada",
            "Cavaleiro Jedi",
            "Rei",
        };
        #endregion

        public IMemoryCache MemoryCache { get; }

        public SummonNameGeneratorService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public Task<string> GetRandomSummonNameAsync()
        {
            var cacheKey = "summonName";
            var cacheExpirationTime = TimeSpan.FromDays(1);

            return MemoryCache.GetOrCreateAsync(cacheKey, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheExpirationTime
                });

                return GenerateRandomName();
            });
        }

        public Task<string> GetRandomMotivationalPhraseAsync()
        {
            var cacheKey = "motivationalPhrase";
            var cacheExpirationTime = TimeSpan.FromDays(1);

            return MemoryCache.GetOrCreateAsync(cacheKey, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = cacheExpirationTime
                });

                return await GenerateRandomMotivationalPhrase();
            });
        }

        public string GenerateRandomName()
        {
            var random = new Random();
            var maximumValue = this.AVAILABLE_SUMMON_NAMES.Count - 1;
            return this.AVAILABLE_SUMMON_NAMES.ElementAt(random.Next(0, maximumValue));
        }

        public async Task<string> GenerateRandomMotivationalPhrase()
        {
            var phraseList = await this.GetPhrasesList();
            var currentDate = DateTime.Today.ToString("dd/MM");
            var todayPhrase = phraseList.First(x => x.Contains(currentDate));

            return todayPhrase.Replace($"({currentDate}) ", string.Empty);
        }

        private async Task<IEnumerable<string>> GetPhrasesList()
        {
            using var webClient = new WebClient();
            var responseHtml = await webClient.DownloadStringTaskAsync("https://crmpiperun.com/blog/frases-motivacionais/");
            var htmlDocument = this.GetHtmlDocument(responseHtml);
            var phrasesListItems = htmlDocument.DocumentNode.SelectNodes("//ol")
                .First(x => x.Id == "frases-motiv")
                .Descendants("li");

            var phraseList = new List<string>();
            foreach (var phraseListItem in phrasesListItems)
            {
                var headerItem = phraseListItem.ChildNodes.First(x => x.Name == "h4");
                phraseList.Add(headerItem.InnerText);
            }

            return phraseList;
        }

        private HtmlDocument GetHtmlDocument(string pageHtml)
        {
            var source = WebUtility.HtmlDecode(pageHtml);
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(source);
            return htmlDocument;
        }
    }
}
