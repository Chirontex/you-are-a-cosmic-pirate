using System;

namespace YaacpShips
{
    namespace Cannons
    {
        class Laser : Cannon
        {
            public Laser(int cannonSize)
            {
                this.Size = cannonSize;
                this.Cooldown = 1;
                this.DamageBase = 10;
                this.DamageMax = 13;
            }
        }
    }
}
