namespace YaacpShips
{
    namespace Vessels
    {
        abstract class Vessel
        {
            public string Name {get; protected set;}
            public int[] Crew {get; protected set;}
            public int[] CrewMax {get; protected set;}
            public string[] CrewTypes {get; protected set;}

            public int Health {get; protected set;}
            public int Size {get; protected set;}

            public Vessel(string shipName, string[] crewTypes, int size, int health = 500)
            {
                this.Name = shipName;
                this.CrewTypes = crewTypes;
                this.Health = health;

                if (size < 1) size = 1;
                else if (size > 3) size = 3;

                this.Size = size;
                this.CrewMax = new int[this.CrewTypes.Length];

                int basicAmount;

                for (var i = 0; i < this.CrewTypes.Length; i++)
                {
                    if (this.CrewTypes[i] == "troops") basicAmount = 40;
                    else if (this.CrewTypes[i] == "sailors") basicAmount = 30;
                    else basicAmount = 20;

                    this.CrewMax[i] = basicAmount * this.Size;
                }
            }

            public void GetDamage(int value)
            {
                if (value > this.Health) this.Health = 0;
                else this.Health -= value;

                if (this.Health == 0)
                {
                    for (var i = 0; i < this.Crew.Length; i++)
                    {
                        this.Crew[i] = 0;
                    }
                }
            }

            public void Repair(int value)
            {
                if (this.Health > 0) this.Health += value;
            }

            public void GetCrew(string type, int number)
            {
                for (var i = 0; i < this.CrewTypes.Length; i++)
                {
                    if (this.CrewTypes[i] == type)
                    {
                        int spaceLeft = this.CrewMax[i] - this.Crew[i];

                        if (spaceLeft >= number) this.Crew[i] += number;
                        else this.Crew[i] += spaceLeft;

                        if (this.Crew[i] <= 0) this.Crew[i] = 0;

                        break;
                    }
                }
            }
        }
    }
}
