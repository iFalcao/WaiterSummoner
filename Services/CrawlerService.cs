using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WaiterSummoner.Services
{
    public class CrawlerService : BaseService
    {
        public IMemoryCache MemoryCache { get; }

        public CrawlerService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public Task<string> GetDailyMotivationalPhraseAsync()
        {
            var cacheKey = "motivationalPhrase";

            return MemoryCache.GetOrCreateAsync(cacheKey, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = base.GetRemainingTimeToday()
                });

                return await GenerateRandomMotivationalPhrase();
            });
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
