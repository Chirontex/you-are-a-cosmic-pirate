using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace VesselsTest
{
    [TestClass]
    public class CorvetteTest
    {
        private Corvette ship = new Corvette("Victoria");

        private void GetArmament()
        {
            ship.Armament = new Cannon[3];
            ship.Armament[0] = new Laser(1);
            ship.Armament[1] = new Kinetic(1);
            ship.Armament[2] = new Rocket(1);
        }

        [TestMethod]
        public void CorvetteParams()
        {
            
        }
    }
}
