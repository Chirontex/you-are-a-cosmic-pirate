using System;
using YaacpPlanets;
using YaacpShips.Cannons;
using YaacpShips.Vessels;

namespace Yaacp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Добро пожаловать в интерфейс управления бортовым компьютером.");
            Console.WriteLine("Это — его первый пользовательский запуск, ваш корабль в данный момент не имеет собственной сигнатуры.");
            Console.Write("Для генерации сигнатуры введите, пожалуйста, имя корабля: ");

            string shipName = Console.ReadLine();
            Vessel ship = new Corvette(shipName);
            ship.Armament = new Cannon[3];

            for (var i = 0; i < ship.Armament.Length; i++)
            {
                ship.Armament[i] = new Laser(1) {Load = true};
            }

            ship.GetCrew("troops", 40);
            ship.GetCrew("sailors", 30);

            int troopsIndex = Array.IndexOf(ship.CrewTypes, "troops");
            int sailorsIndex = Array.IndexOf(ship.CrewTypes, "sailors");

            Program.OnPlanet(ship, 10000, new Earth());
        }

        static void GuiGenerate(Vessel ship, int credits)
        {
            string[] gui = new string[5];
            gui[0] = "Корабль: ";

            string shipClass = ship is Corvette ? "Корвет" : null;
            shipClass = ship is Frigate ? "Фригат" : shipClass;
            shipClass = ship is Dreadnought ? "Дредноут" : shipClass;

            gui[0] += shipClass+" \""+ship.Name+"\" | Здоровье: "+ship.Health+"/"+ship.HealthMax;

            gui[1] = "Орудия: ";

            for (var i = 0; i < ship.Armament.Length; i++)
            {
                if (ship.Armament[i] is Laser) gui[1] += "Лазерное ";
                else if (ship.Armament[i] is Kinetic) gui[1] += "Кинетическое ";
                else if (ship.Armament[i] is Rocket) gui[1] += "Ракетомёт ";

                gui[1] += ship.Armament[i].Size;

                if ((ship.Armament.Length - i) > 1) gui[1] += " | ";
            }

            gui[2] = "Команда: ";

            for (var i = 0; i < ship.CrewTypes.Length; i++)
            {
                if (ship.CrewTypes[i] == "troops") gui[2] += "Солдаты — ";
                else if (ship.CrewTypes[i] == "sailors") gui[2] += "Матросы — ";
                else if (ship.CrewTypes[i] == "medics") gui[2] += "Медики — ";
                else if (ship.CrewTypes[i] == "mechanics") gui[2] += "Механики — ";

                gui[2] += ship.Crew[i]+"/"+ship.CrewMax[i];

                if ((ship.CrewTypes.Length - i) > 1) gui[2] += " | ";
            }

            if (ship.Status == "Nothing") gui[3] = "Можно взять задание";
            else gui[3] = "Задания недоступны; сперва сдайте уже взятое";

            gui[4] = "Кредиты: "+credits;

            for (var i = 0; i < gui.Length; i++)
            {
                Console.WriteLine(gui[i]);
            }

            Console.Write("\n");
        }

        static void OnPlanet(Vessel ship, int credits, Planet planet)
        {
            Console.Clear();

            Program.GuiGenerate(ship, credits);

            Console.WriteLine("Вы находитесь на планете "+PlanetParameters.PlanetName(planet, "ru")+". Куда бы вы хотели отправиться?");
            Console.WriteLine("[1] — Верфь");
            Console.WriteLine("[2] — Транспортная компания");
            Console.WriteLine("[3] — Биржа труда");
            Console.WriteLine("[4] — Космопорт (улететь отсюда)");
            Console.Write("Выберите один из вариантов: ");
            Console.ReadKey();
        }
    }
}
