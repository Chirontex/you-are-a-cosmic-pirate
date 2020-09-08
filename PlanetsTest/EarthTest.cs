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

        [TestMethod]
        public void TestShipStatusGiveQuest()
        {
            this.TestInit();
            this.planet.GiveQuest(this.ship);

            Assert.AreNotEqual("Nothing", this.ship.Status,
                String.Format("Quest not given, because ship status still is {0}.",
                    this.ship.Status));
        }

        [TestMethod]
        public void TestShipStatusTakeQuest()
        {
            this.TestInit();
            this.planet.GiveQuest(this.ship);
            this.planet.TakeQuest(this.ship);

            string expected = "Nothing";

            Assert.AreEqual(expected, this.ship.Status,
                String.Format("Expected ship status: {0}; actual: {1}.",
                    expected, this.ship.Status));
        }
    }
}
