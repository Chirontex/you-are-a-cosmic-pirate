namespace YaacpShips
{
    namespace Vessels
    {
        public class Dreadnought : Vessel
        {
            public Dreadnought(string shipName) : base(shipName, new string[4] {"troops", "sailors", "medics", "mechanics"}, 3, BasicParameters.HealthBaseDefault)
            {}
        }
    }
}
