using System;
using YaacpShips;
using YaacpShips.Vessels;
using YaacpShips.Cannons;

namespace YaacpPlanets
{
    public abstract class Planet
    {   
        public string[] ApplicantsTypes {get; private set;}
        public int[] ApplicantsAmount {get; protected set;}
        public int MaturityLevel {get; protected set;}
        public Vessel[] SecondhandShips {get; protected set;}

        public Planet(int maturityLevel)
        {
            Random randomizer = new Random();

            if (maturityLevel < 1) maturityLevel = 1;
            else if (maturityLevel > 3) maturityLevel = 3;

            this.MaturityLevel = maturityLevel;
            this.ApplicantsTypes = new string[4] {"troops", "sailors", "medics", "mechanics"};
            this.ApplicantsAmount = new int[this.ApplicantsTypes.Length];
            int max;

            for (var i = 0; i < this.ApplicantsTypes.Length; i++)
            {
                if (this.ApplicantsTypes[i] == "troops") max = 50;
                else if (this.ApplicantsTypes[i] == "sailors") max = 25;
                else max = 10;

                max = max * 2 * this.MaturityLevel;
                this.ApplicantsAmount[i] = randomizer.Next(0, max);
            }

            this.SecondhandShips = new Vessel[3 * this.MaturityLevel];
            string shipName;

            for (var i = 0; i < this.SecondhandShips.Length; i++)
            {
                shipName = BasicParameters.DefaultShipNames[randomizer.Next(0, BasicParameters.DefaultShipNames.Length - 1)];

                switch (randomizer.Next(1, 3))
                {
                    case 1:
                    this.SecondhandShips[i] = new Corvette(shipName);
                    this.SecondhandShips[i].Armament = new Cannon[3];
                    break;

                    case 2:
                    this.SecondhandShips[i] = new Frigate(shipName);
                    this.SecondhandShips[i].Armament = new Cannon[6];
                    break;

                    case 3:
                    this.SecondhandShips[i] = new Dreadnought(shipName);
                    this.SecondhandShips[i].Armament = new Cannon[9];
                    break;
                }

                for (var c = 0; c < this.SecondhandShips[i].Armament.Length; c++)
                {
                    switch (randomizer.Next(1, 3))
                    {
                        case 1:
                        this.SecondhandShips[i].Armament[c] = new Laser(randomizer.Next(1, this.SecondhandShips[i].Size)) {Load = true};
                        break;

                        case 2:
                        this.SecondhandShips[i].Armament[c] = new Kinetic(randomizer.Next(1, this.SecondhandShips[i].Size)) {Load = true};
                        break;

                        case 3:
                        this.SecondhandShips[i].Armament[c] = new Rocket(randomizer.Next(1, this.SecondhandShips[i].Size)) {Load = true};
                        break;
                    }
                }

                this.SecondhandShips[i].GetDamage(randomizer.Next(1, this.SecondhandShips[i].HealthMax - 1));
                this.SecondhandShips[i].Status = "For sale";
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

        public void RepairShip(Vessel ship, int value)
        {
            ship.Repair(value);
        }


    }
}
