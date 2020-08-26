using YaacpShips.Vessels;

namespace YaacpShips
{
    public static class BasicParameters
    {
        public static int HealthBaseDefault => 500;
        private static int TroopsPowerMod = 3;
        private static int SailorsPowerMod = 2;
        private static int OthersPowerMod = 1;

        public static int BoardingPower(Vessel ship)
        {
            int result = 0;
            int mod;

            for (var i = 0; i < ship.Crew.Length; i++)
            {
                if (ship.CrewTypes[i] == "troops") mod = BasicParameters.TroopsPowerMod;
                else if (ship.CrewTypes[i] == "sailors") mod = BasicParameters.SailorsPowerMod;
                else mod = BasicParameters.OthersPowerMod;

                result += ship.Crew[i] * mod;
            }

            return result;
        }

        public static int[] CrewRest(int power, string[] crewTypes, int[] crewMax)
        {
            int[] result = new int[crewTypes.Length];

            

            return result;
        }
    }
}
