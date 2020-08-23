using System;

namespace YaacpShips
{
    namespace Cannons
    {
        class Rocket : Cannon
        {
            private override int damageBase = 0;
            private override int damageMax = 35;
            private override int cooldown = 3;
        }
    }
}
