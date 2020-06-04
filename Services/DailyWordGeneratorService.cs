using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WaiterSummoner.Services
{
    public class DailyWordGeneratorService : BaseService
    {
        public IMemoryCache MemoryCache { get; }

        public DailyWordGeneratorService(IMemoryCache memoryCache)
        {
            MemoryCache = memoryCache;
        }

        public Task<string> GetRandomWordAsync()
        {
            var cacheKey = "dailyWord";

            return MemoryCache.GetOrCreateAsync(cacheKey, async e =>
            {
                e.SetOptions(new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = base.GetRemainingTimeToday()
                });

                return GenerateRandomName();
            });
        }

        public string GenerateRandomName()
        {
            var random = new Random();
            var maximumValue = this.AVAILABLE_SUMMON_NAMES.Count - 1;
            return this.AVAILABLE_SUMMON_NAMES.ElementAt(random.Next(0, maximumValue));
        }

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
    }
}
