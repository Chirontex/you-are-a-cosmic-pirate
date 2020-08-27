using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace VesselsTest
{
    [TestClass]
    public class CorvetteTest
    {
        private Corvette shipOne = new Corvette("Victoria");
        private Corvette shipTwo = new Corvette("Sedna");

        public CorvetteTest()
        {
            shipOne.Armament = new Cannon[3];
            shipOne.Armament[0] = new Laser(1) {Load = true};
            shipOne.Armament[1] = new Kinetic(1) {Load = true};
            shipOne.Armament[2] = new Rocket(1) {Load = true};

            shipOne.GetCrew("troops", 40);
            shipOne.GetCrew("sailors", 30);

            shipTwo.Armament = new Cannon[3];
            shipTwo.Armament[0] = new Rocket(1) {Load = true};
            shipTwo.Armament[1] = new Rocket(1) {Load = true};
            shipTwo.Armament[2] = new Rocket(1) {Load = true};

            shipTwo.GetCrew("troops", 30);
            shipTwo.GetCrew("sailors", 20);
        }

        [TestMethod]
        public void CorvetteGetDamageByFire()
        {
            shipTwo.GetDamage(shipOne.Volley());

            Assert.AreNotEqual(shipTwo.HealthMax, shipTwo.Health,
                String.Format("Health are still equal to maximum health. Health: {0}, HealthMax: {1}",
                    shipTwo.Health, shipTwo.HealthMax));

            for (var i = 0; i < shipOne.Armament.Length; i++)
            {
                Assert.AreNotEqual(true, shipOne.Armament[i].Load,
                    String.Format("Cannon {0} is still loaded after volley.", i));

                Assert.AreEqual(shipOne.Armament[i].Cooldown, shipOne.Armament[i].CooldownCount,
                    String.Format("Expected for cannon cooldown counter: {0}; actual: {1}",
                        shipOne.Armament[i].Cooldown, shipOne.Armament[i].CooldownCount));
            }
        }

        [TestMethod]
        public void CorvetteBattleByDeath()
        {
            while (shipOne.Health > 0 || shipTwo.Health > 0)
            {
                shipOne.GetDamage(shipTwo.Volley());
                shipTwo.GetDamage(shipOne.Volley());
                shipOne.ArmamentReload();
                shipTwo.ArmamentReload();
            }

            Vessel looser = shipOne.Health == 0 ? shipOne : shipTwo;
            bool looserCannonsWorking = true;

            for (var i = 0; i < looser.Armament.Length; i++)
            {
                looserCannonsWorking = looserCannonsWorking && looser.Armament[i].Working;
            }

            Assert.IsTrue(looserCannonsWorking,
                String.Format("Expected looser cannons working: false; actual: {0}",
                    looserCannonsWorking));
        }
    }
}
