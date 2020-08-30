using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace VesselsTest
{
    [TestClass]
    public class CorvetteTest
    {
        private Corvette ShipOne {get; set;}
        private Corvette ShipTwo {get; set;}

        public void CorvettesInit()
        {
            this.ShipOne = new Corvette("Celestia");
            this.ShipOne.Armament = new Cannon[3];
            this.ShipOne.Armament[0] = new Laser(1) {Load = true};
            this.ShipOne.Armament[1] = new Kinetic(1) {Load = true};
            this.ShipOne.Armament[2] = new Rocket(1) {Load = true};

            this.ShipOne.GetCrew("troops", 40);
            this.ShipOne.GetCrew("sailors", 30);

            this.ShipTwo = new Corvette("Luna");
            this.ShipTwo.Armament = new Cannon[3];
            this.ShipTwo.Armament[0] = new Rocket(1) {Load = true};
            this.ShipTwo.Armament[1] = new Rocket(1) {Load = true};
            this.ShipTwo.Armament[2] = new Rocket(1) {Load = true};

            this.ShipTwo.GetCrew("troops", 30);
            this.ShipTwo.GetCrew("sailors", 20);
        }

        [TestMethod]
        public void CorvetteGetDamageByFire()
        {
            this.CorvettesInit();
            this.ShipTwo.GetDamage(this.ShipOne.Volley());

            Assert.AreNotEqual(this.ShipTwo.HealthMax, this.ShipTwo.Health,
                String.Format("Health are still equal to maximum health. Health: {0}, HealthMax: {1}",
                    this.ShipTwo.Health, this.ShipTwo.HealthMax));

            for (var i = 0; i < this.ShipOne.Armament.Length; i++)
            {
                Assert.AreNotEqual(true, this.ShipOne.Armament[i].Load,
                    String.Format("Cannon {0} is still loaded after volley.", i));

                Assert.AreEqual(this.ShipOne.Armament[i].Cooldown, this.ShipOne.Armament[i].CooldownCount,
                    String.Format("Expected for cannon cooldown counter: {0}; actual: {1}",
                        this.ShipOne.Armament[i].Cooldown, this.ShipOne.Armament[i].CooldownCount));
            }
        }

        [TestMethod]
        public void CorvetteBattleByDeath()
        {
            this.CorvettesInit();

            while ((this.ShipOne.Health > 0) && (this.ShipTwo.Health > 0))
            {
                this.ShipOne.GetDamage(this.ShipTwo.Volley());
                this.ShipTwo.GetDamage(this.ShipOne.Volley());
                this.ShipOne.ArmamentReload();
                this.ShipTwo.ArmamentReload();
            }

            Vessel looser = this.ShipOne.Health == 0 ? this.ShipOne : this.ShipTwo;
            bool looserCannonsWorking = true;

            for (var i = 0; i < looser.Armament.Length; i++)
            {
                looserCannonsWorking = looserCannonsWorking && looser.Armament[i].Working;
            }

            Assert.IsFalse(looserCannonsWorking,
                String.Format("Expected looser cannons working: false; actual: {0}",
                    looserCannonsWorking));

            for (var i = 0; i < looser.Crew.Length; i++)
            {
                Assert.AreEqual(0, looser.Crew[i],
                    String.Format("Expected looser crew amount: 0; actual: {0}",
                        looser.Crew[i]));
            }
        }

        public void CorvetteBoarding()
        {
            this.CorvettesInit();
            int[] shipOneCrewAmount = this.ShipOne.Crew;
            this.ShipOne.Boarding(this.ShipTwo);

            for (var i = 0; i < this.ShipOne.Crew.Length; i++)
            {
                Assert.AreNotEqual(shipOneCrewAmount[i], this.ShipOne.Crew[i],
                    String.Format("Winner crew amount not expected: {0}; actual {1}",
                        shipOneCrewAmount[i], this.ShipOne.Crew[i]));
            }

            for (var i = 0; i < this.ShipTwo.Crew.Length; i++)
            {
                Assert.AreEqual(0, this.ShipTwo.Crew[i],
                    String.Format("Looser crew amount expected: 0; actual: {0}",
                        this.ShipTwo.Crew[i]));
            }
        }
    }
}
