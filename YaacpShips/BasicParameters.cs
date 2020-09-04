using YaacpShips.Vessels;

namespace YaacpShips
{
    public static class BasicParameters
    {
        private static int troopsPowerMod = 3;
        private static int sailorsPowerMod = 2;
        private static int othersPowerMod = 1;

        public static int HealthBaseDefault => 500;
        public static string[] DefaultShipNames => new string[] {
            "Lola",
            "Rosetta",
            "Celestia",
            "Hecate",
            "Eridae",
            "Queen Anna's Revenge",
            "Enterprise",
            "Sentinel",
            "Bearded Bastard",
            "Black Knight",
            "Cyber Bieber",
            "Luna",
            "Sepia",
            "Seline",
            "Mayflower",
            "Mandalorian",
            "Apostle",
            "Galadriel",
            "Gandalf",
            "Boromir",
            "Vladimir",
            "Admiral Kuznetsoff",
            "Magus"
        };

        public static int BoardingPower(Vessel ship)
        {
            int result = 0;
            int mod;

            for (var i = 0; i < ship.Crew.Length; i++)
            {
                if (ship.CrewTypes[i] == "troops") mod = BasicParameters.troopsPowerMod;
                else if (ship.CrewTypes[i] == "sailors") mod = BasicParameters.sailorsPowerMod;
                else mod = BasicParameters.othersPowerMod;

                result += ship.Crew[i] * mod;
            }

            return result;
        }

        public static int[] CrewRest(int powerStart, int powerRest, string[] crewTypes, int[] crew)
        {
            int[] result = new int[crewTypes.Length];
            int powerPercentage = ((powerStart - powerRest) * 100) / powerStart;
            int mod;

            for (var i = 0; i < crewTypes.Length; i++)
            {
                if (crewTypes[i] == "troops") mod = BasicParameters.troopsPowerMod;
                else if (crewTypes[i] == "sailors") mod = BasicParameters.sailorsPowerMod;
                else mod = BasicParameters.othersPowerMod;

                result[i] = crew[i] - (((crew[i] * 100) / powerPercentage) / mod);
            }

            return result;
        }

        public static int[] CrewRest(int powerPercentage, string[] crewTypes, int[] crew)
        {
            int[] result = new int[crewTypes.Length];
            int mod;

            for (var i = 0; i < crewTypes.Length; i++)
            {
                if (crewTypes[i] == "troops") mod = BasicParameters.troopsPowerMod;
                else if (crewTypes[i] == "sailors") mod = BasicParameters.sailorsPowerMod;
                else mod = BasicParameters.othersPowerMod;

                result[i] = crew[i] - (((crew[i] * 100) / powerPercentage) / mod);
            }

            return result;
        }
    }
}
