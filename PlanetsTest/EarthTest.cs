using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaacpShips.Vessels;
using YaacpShips.Cannons;
using YaacpPlanets;

namespace PlanetsTest
{
    [TestClass]
    public class EarthTest
    {
        private Earth planet {get; set;}
        private Frigate ship {get; set;}

        [TestMethod]
        public void TestInit()
        {
            this.planet = new Earth();
            this.ship = new Frigate("Mephisto");
            this.ship.Armament = new Cannon[6];
            
            for (var i = 0; i < this.ship.Armament.Length; i++)
            {
                this.ship.Armament[i] = new Kinetic(2) {Load = true};
            }
        }
    }
}
