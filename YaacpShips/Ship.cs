using System;

namespace YaacpShips
{
    abstract class Ship
    {
        private int health;
        private string[] crewTypes = {"troops", "sailors"};
        private int[] crew = {0, 0};
        private int[] crewMax = {0, 0};
        public string Name {get; set;}

        public void GetDamage(int value)
        {
            if (value > health) health = 0;
            else health -= value;

            if (health == 0)
            {
                for (var i = 0; i < crew.Length; i++)
                {
                    crew[i] = 0;
                }
            }
        }

        public void Repair(int value)
        {
            if (health > 0) health += value;
        }

        public void GetCrew(string type, int number)
        {
            for (var i = 0; i < crewTypes.Length; i++)
            {
                if (crewTypes[i] == type)
                {
                    int spaceLeft = crewMax[i] - crew[i];

                    if (spaceLeft >= number) crew[i] += number;
                    else crew[i] += spaceLeft;

                    break;
                }
            }
        }
    }
}
