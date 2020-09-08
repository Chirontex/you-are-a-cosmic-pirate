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

            for (var i = 0; i < this.ship.CrewTypes.Length; i++)
            {
                if (this.ship.CrewTypes[i] == "troops") this.ship.Crew[i] = 50;
                else if (this.ship.CrewTypes[i] == "sailors") this.ship.Crew[i] = 40;
                else this.ship.Crew[i] = 6;
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

        [TestMethod]
        public void TestFiring()
        {
            this.TestInit();

            int medicsOnPlanetIndex = Array.IndexOf(this.planet.ApplicantsTypes, "medics");
            
            Assert.AreNotEqual(medicsOnPlanetIndex, (this.planet.ApplicantsTypes.GetLowerBound(0) - 1),
                String.Format("Medics on planet index must not be equal to {0}, but it does.",
                    (this.planet.ApplicantsTypes.GetLowerBound(0) - 1)));

            int medicsOnPlanet = this.planet.ApplicantsAmount[medicsOnPlanetIndex];
            int medicsOnShip = this.ship.Crew[Array.IndexOf(this.ship.CrewTypes, "medics")];

            this.planet.Firing(this.ship, "medics", 5);

            Assert.AreEqual((medicsOnPlanet + 5), this.planet.ApplicantsAmount[medicsOnPlanetIndex],
                String.Format("Medics on planet amount expected: {0}; actual: {1}.",
                    (medicsOnPlanet + 5), this.planet.ApplicantsAmount[medicsOnPlanetIndex]));

            Assert.AreEqual(medicsOnShip, (this.ship.Crew[Array.IndexOf(this.ship.CrewTypes, "medics")] + 5),
                String.Format("Medics on ship expected: {0}; actual: {1}.",
                    medicsOnShip, (this.planet.ApplicantsAmount[Array.IndexOf(this.ship.CrewTypes, "medics")] + 5)));
        }

        [TestMethod]
        public void TestHiring()
        {
            this.TestInit();

            int medicsOnPlanetIndex = Array.IndexOf(this.planet.ApplicantsTypes, "medics");
            int medicsOnShipIndex = Array.IndexOf(this.ship.CrewTypes, "medics");

            int medicsOnPlanet = this.planet.ApplicantsAmount[medicsOnPlanetIndex];
            int medicsOnShip = this.ship.Crew[medicsOnShipIndex];

            this.planet.Firing(this.ship, "medics", 5);
            this.planet.Hiring(this.ship, "medics", 5);

            Assert.AreEqual(medicsOnPlanet, this.planet.ApplicantsAmount[medicsOnPlanetIndex],
                String.Format("Medics on planet expected: {0}; actual: {1}.",
                    medicsOnPlanet, this.planet.ApplicantsAmount[medicsOnPlanetIndex]));

            Assert.AreEqual(medicsOnShip, this.ship.Crew[medicsOnShipIndex],
                String.Format("Medics on ship expected: {0}; actual: {1}.",
                    medicsOnShip, this.ship.Crew[medicsOnShipIndex]));
        }
    }
}
