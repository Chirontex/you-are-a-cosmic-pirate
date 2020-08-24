using System;

namespace YaacpShips
{
    namespace Cannons
    {
        class Kinetic : Cannon
        {
            public Kinetic(int cannonSize)
            {
                this.Size = cannonSize;
                this.Cooldown = 3;
                this.DamageBase = 10;
                this.DamageMax = 20;
            }
        }
    }
}
