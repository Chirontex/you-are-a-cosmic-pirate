using System;

namespace YaacpShips
{
    namespace Cannons
    {
        abstract class Cannon
        {
            private bool working = true;
            private bool load = false;
            private int size;
            private int cooldown;
            private int cooldownCount = 0;
            private int damageBase;
            private int damageMax;
            
            public bool Working
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
                protected set
                {
                    cooldown = value;
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

            public int DamageBase
            {
                get
                {
                    return damageBase;
                }
                protected set
                {
                    damageBase = value;
                }
            }

            public int DamageMax
            {
                get
                {
                    return damageMax;
                }
                set
                {
                    damageMax = value;
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
