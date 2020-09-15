using System;

namespace YaacpPlanets
{
    public static class PlanetParameters
    {
        public static string PlanetName(object planet, string lang = "en")
        {
            string[] planetsEn = new string[] {"Mercury", "Venus", "Earth", "Moon", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto", "unknown"};
            string[] planetsRu = new string[] {"Меркурий", "Венера", "Земля", "Луна", "Марс", "Юпитер", "Сатурн", "Уран", "Нептун", "Плутон", "неизвестная"};

            if (planet is Planet)
            {
                string planetName = planet is Mercury ? planetsEn[0] : null;
                planetName = planet is Venus ? planetsEn[1] : planetName;
                planetName = planet is Earth ? planetsEn[2] : planetName;
                planetName = planet is Moon ? planetsEn[3] : planetName;
                planetName = planet is Mars ? planetsEn[4] : planetName;
                planetName = planet is Jupiter ? planetsEn[5] : planetName;
                planetName = planet is Saturn ? planetsEn[6] : planetName;
                planetName = planet is Uranus ? planetsEn[7] : planetName;
                planetName = planet is Neptune ? planetsEn[8] : planetName;
                planetName = planet is Pluto ? planetsEn[9] : planetName;

                if (lang == "ru") return planetsRu[Array.IndexOf(planetsEn, planetName)];
                else return planetName;
            }
            else
            {
                if (lang == "ru") return planetsRu[10];
                else return planetsEn[10];
            }
        }
    }
}
