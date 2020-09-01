namespace YaacpShips
{
    namespace Vessels
    {
        public class Frigate : Vessel
        {
            public Frigate(string shipName) : base(shipName, new string[3] {"troops", "sailors", "medics"}, 2, BasicParameters.HealthBaseDefault)
            {}
        }
    }
}
