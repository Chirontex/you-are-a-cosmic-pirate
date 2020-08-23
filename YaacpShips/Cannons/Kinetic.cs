using System;

namespace YaacpShips
{
    namespace Cannons
    {
        class Kinetic : Cannon
        {
            private override int damageBase = 8;
            private override int damageMax = 20;
            private override int cooldown = 2;
        }
    }
}
