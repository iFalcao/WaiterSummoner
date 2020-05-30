using System;
using System.Collections.Generic;
using System.Linq;

namespace WaiterSummoner.Services
{
    public static class SummonNameGenerator
    {
        #region Props
        private static HashSet<string> AVAILABLE_SUMMON_NAMES = new HashSet<string>
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

        public static string GetRandomSummonName()
        {
            var random = new Random();
            var maximumCount = AVAILABLE_SUMMON_NAMES.Count;
            return AVAILABLE_SUMMON_NAMES.ElementAt(random.Next(0, maximumCount));
        }
    }
}
