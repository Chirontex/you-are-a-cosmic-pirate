using System;

namespace YaacpShips
{
    namespace Cannons
    {
        abstract class Cannon
        {
            private bool working = true;
            private bool load = false;
            private int size = 1;
            private virtual int cooldown = 1;
            private int cooldownCount = 0;
            private virtual int damageBase = 1;
            private virtual int damageMax = 1;
            
            private bool Working
            {
                get
                {
                    return working;
                }
                set
                {
                    working = value;

                    if (!working) load = false;
                }
            }

            public bool Load
            {
                get
                {
                    return load;
                }
                set
                {
                    load = working && value;

                    if (!load) this.CooldownCount = cooldown;
                }
            }

            public int Size
            {
                get
                {
                    return size;
                }
                set
                {
                    if (value < 1) size = 1;
                    else if (value > 3) size = 3;
                    else size = value;
                }
            }

            public int Cooldown
            {
                get
                {
                    return cooldown;
                }
            }
            public int CooldownCount
            {
                get
                {
                    return cooldownCount;
                }
                set
                {
                    if (working && value <= cooldown)
                    {
                        cooldownCount = value;

                        if (cooldownCount == 0) this.Load = true;
                    }
                }
            }

            public int Fire(Random randomizer)
            {
                int result;

                if (working && load) result = randomizer.Next((size * damageBase), (size * damageMax));
                else result = 0;

                return result;
            }
        }
    }
}
