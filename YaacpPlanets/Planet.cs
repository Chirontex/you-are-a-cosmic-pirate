using System;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace YaacpPlanets
{
    public abstract class Planet
    {   
        public string[] ApplicantsTypes {get; private set;}
        public int[] ApplicantsAmount {get; protected set;}

        public Planet()
        {
            Random randomizer = new Random();
            this.ApplicantsTypes = new string[4] {"troops", "sailors", "medics", "mechanics"};
            this.ApplicantsAmount = new int[this.ApplicantsTypes.Length];
            int max;

            for (var i = 0; i < this.ApplicantsTypes.Length; i++)
            {
                if (this.ApplicantsTypes[i] == "troops") max = 500;
                else if (this.ApplicantsTypes[i] == "sailors") max = 250;
                else max = 100;

                this.ApplicantsAmount[i] = randomizer.Next(0, max);
            }
        }

        public void Hiring(Vessel ship, string type, int hireAmount)
        {
            int index = Array.IndexOf(this.ApplicantsTypes, type);
            int basicAmount = this.ApplicantsAmount[index];

            if (hireAmount > basicAmount) hireAmount = basicAmount;

            ship.GetCrew(type, hireAmount);
            this.ApplicantsAmount[index] = basicAmount - hireAmount;
        }

        public void Firing(Vessel ship, string type, int fireAmount)
        {
            int indexApplicants = Array.IndexOf(this.ApplicantsTypes, type);
            int indexCrew = Array.IndexOf(ship.CrewTypes, type);
            int crew = ship.Crew[indexCrew];

            if (fireAmount > crew) fireAmount = crew;

            ship.GetCrew(type, -fireAmount);
            this.ApplicantsAmount[indexApplicants] += fireAmount;
        }
    }
}
