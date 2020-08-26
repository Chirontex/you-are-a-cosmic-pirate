namespace YaacpShips
{
    namespace Vessels
    {
        public class Corvette : Vessel
        {
            public Corvette(string shipName) : base(shipName, new string[2] {"troops", "sailors"}, 1, BasicParameters.HealthBaseDefault)
            {}
        }
    }
}
