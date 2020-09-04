﻿using System;
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

            int indexCrew = Array.IndexOf(ship.CrewTypes, type);
            int crewAmount = ship.Crew[indexCrew];
            
            ship.GetCrew(type, hireAmount);

            int crewDifference = crewAmount - ship.Crew[indexCrew];
            this.ApplicantsAmount[index] = basicAmount - crewDifference;
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

        public void TradeShip(Vessel ship, int newShipNumber)
        {
            Vessel cloneShipOld = this.CloneShip(ship);
            Vessel cloneShipNew = this.CloneShip(this.SecondhandShips[newShipNumber]);

            string[] crewTypesOld = ship.CrewTypes;
            int[] crewAmountsOld = new int[ship.Crew.Length];

            for (var i = 0; i < ship.Crew.Length; i++)
            {
                crewAmountsOld[i] = ship.Crew[i];
            }

            for (var i = 0; i < ship.CrewTypes.Length; i++)
            {
                this.Firing(ship, ship.CrewTypes[i], ship.Crew[i]);
            }

            ship = cloneShipNew;

            for (var i = 0; i < cloneShipNew.CrewTypes.Length; i++)
            {

            }
        }

        protected Vessel CloneShip(Vessel original)
        {
            Vessel clone;
            clone = original is Corvette ? new Corvette(original.Name) : null;
            clone = original is Frigate ? new Frigate(original.Name) : null;
            clone = original is Dreadnought ? new Dreadnought(original.Name) : null;

            clone.Armament = new Cannon[original.Armament.Length];

            for (var i = 0; i < original.Armament.Length; i++)
            {
                clone.Armament[i] = original.Armament[i] is Laser ? new Laser(original.Armament[i].Size) {Load = true} : null;
                clone.Armament[i] = original.Armament[i] is Kinetic ? new Kinetic(original.Armament[i].Size) {Load = true} : null;
                clone.Armament[i] = original.Armament[i] is Rocket ? new Rocket(original.Armament[i].Size) {Load = true} : null;

                clone.Armament[i].Load = original.Armament[i].Load;
                clone.Armament[i].CooldownCount = original.Armament[i].CooldownCount;
                clone.Armament[i].Working = original.Armament[i].Working;
            }

            clone.GetDamage(original.HealthMax - original.Health);
            clone.Status = "Clone";

            return clone;
        }
    }
}
