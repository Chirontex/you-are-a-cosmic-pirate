using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YaacpShips.Cannons;

namespace CannonsTest
{
    [TestClass]
    public class CannonsUnitTestLaser
    {
        private Laser cannon = new Laser(1) {Load = true};

        [TestMethod]
        public void TestCooldown()
        {
            Assert.AreEqual(cannon.Cooldown, 1,
                String.Format("Expected for cannon cooldown: 1; actual: {0}",
                    cannon.Cooldown));
        }

        [TestMethod]
        public void TestCooldownCount()
        {
            Assert.AreEqual(cannon.CooldownCount, 0,
                String.Format("Expected for cannon cooldown count: 0; actual: {0}",
                    cannon.CooldownCount));
        }

        [TestMethod]
        public void TestFire()
        {
            var randomizer = new Random();
            int[] fires = new int[4];

            fires[0] = cannon.Fire(randomizer);
            fires[1] = cannon.Fire(randomizer);

            cannon.CooldownCount = 10;

            fires[2] = cannon.Fire(randomizer);

            cannon.CooldownCount = 0;

            fires[3] = cannon.Fire(randomizer);

            int expectations = 0;

            for (var i = 0; i < fires.Length; i++)
            {
                if (i == 0 || i == 3)
                {
                    Assert.AreNotEqual(fires[i], expectations,
                        String.Format("Expected for firing result: not {0}; actual: {1}",
                            expectations, fires[i]));
                }
                else
                {
                    Assert.AreEqual(fires[i], expectations,
                        String.Format("Expected for firing result: {0}; actual: {1}",
                            expectations, fires[i]));
                }
            }
        }
    }
}
