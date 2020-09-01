using System;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace YaacpPlanets
{
    public abstract class Planet
    {
        private string[] applicantsTypes = new string[4] {"troops", "sailors", "medics", "mechanics"};
        
        public string[] ApplicantsTypes
        {
            get
            {
                return applicantsTypes;
            }
        }

        public int[] ApplicantsAmount;

        public Planet()
        {
            Random randomizer = new Random();

            this.ApplicantsAmount = new int[this.ApplicantsTypes.Length];

            for (var i = 0; i < this.ApplicantsTypes.Length; i++)
            {
                if (this.ApplicantsTypes[i] == "troops") this.ApplicantsAmount[i] = randomizer.Next(0, 500);
                else if (this.ApplicantsTypes[i] == "sailors") this.ApplicantsAmount[i] = randomizer.Next(0, 250);
                else this.ApplicantsAmount[i] = randomizer.Next(0, 100);
            }
        }

        public void Hire(Vessel ship, string type, int amount)
        {
            int basicAmount = this.ApplicantsAmount[Array.IndexOf(this.ApplicantsTypes, type)];


        }
    }
}
