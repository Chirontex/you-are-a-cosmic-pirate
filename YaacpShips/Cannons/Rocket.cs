using System;

namespace YaacpShips
{
    namespace Cannons
    {
        class Rocket : Cannon
        {
            public Rocket(int cannonSize)
            {
                this.Size = cannonSize;
                this.Cooldown = 5;
                this.DamageBase = 0;
                this.DamageMax = 40;
            }
        }
    }
}
