using System;

namespace YaacpShips
{
    namespace Cannons
    {
        public abstract class Cannon
        {
            private bool working = true;
            private bool load = false;
            private int size;
            private int cooldownCount = 0;
            
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

                    if (!load) this.CooldownCount = this.Cooldown;
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

            public int Cooldown {get; protected set;}
            public int CooldownCount
            {
                get
                {
                    return cooldownCount;
                }
                set
                {
                    if (working && (value <= this.Cooldown))
                    {
                        cooldownCount = value;

                        if (cooldownCount == 0) this.Load = true;
                    }
                }
            }

            public int DamageBase {get; protected set;}

            public int DamageMax {get; set;}

            public int Fire(Random randomizer)
            {
                int result;

                if (working && load)
                {
                    result = randomizer.Next((size * this.DamageBase), (size * this.DamageMax));
                    this.Load = false;
                }
                else result = 0;

                return result;
            }
        }
    }
}
