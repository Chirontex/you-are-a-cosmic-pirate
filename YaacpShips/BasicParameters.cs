using YaacpShips.Vessels;

namespace YaacpShips
{
    public static class BasicParameters
    {
        public static int HealthBaseDefault => 500;

        public static int BoardingPower(Vessel ship)
        {
            int result = 0;
            int mod;

            for (var i = 0; i < ship.Crew.Length; i++)
            {
                if (ship.CrewTypes[i] == "troops") mod = 3;
                else if (ship.CrewTypes[i] == "sailors") mod = 2;
                else mod = 1;

                result += ship.Crew[i] * mod;
            }

            return result;
        }
    }
}
