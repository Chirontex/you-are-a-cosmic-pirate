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

        static string[] GuiGenerate(Vessel ship, int credits)
        {
            string[] result = new string[5];
            result[0] = "Корабль: ";

            string shipClass = ship is Corvette ? "Корвет" : null;
            shipClass = ship is Frigate ? "Фригат" : shipClass;
            shipClass = ship is Dreadnought ? "Дредноут" : shipClass;

            result[0] += shipClass+" \""+ship.Name+"\" | Здоровье: "+ship.Health+"/"+ship.HealthMax;

            result[1] = "Орудия: ";

            for (var i = 0; i < ship.Armament.Length; i++)
            {
                if (ship.Armament[i] is Laser) result[1] += "Лазерное ";
                else if (ship.Armament[i] is Kinetic) result[1] += "Кинетическое ";
                else if (ship.Armament[i] is Rocket) result[1] += "Ракетомёт ";

                result[1] += ship.Armament[i].Size;

                if ((ship.Armament.Length - i) > 1) result[1] += " | ";
            }

            result[2] = "Команда: ";

            for (var i = 0; i < ship.CrewTypes.Length; i++)
            {
                if (ship.CrewTypes[i] == "troops") result[2] += "Солдаты — ";
                else if (ship.CrewTypes[i] == "sailors") result[2] += "Матросы — ";
                else if (ship.CrewTypes[i] == "medics") result[2] += "Медики — ";
                else if (ship.CrewTypes[i] == "mechanics") result[2] += "Механики — ";

                result[2] += ship.Crew[i]+"/"+ship.CrewMax[i];

                if ((ship.CrewTypes.Length - i) > 1) result[2] += " | ";
            }

            if (ship.Status == "Nothing") result[3] = "Можно взять задание";
            else result[3] = "Задания недоступны; сперва сдайте уже взятое";

            result[4] = "Кредиты: "+credits;

            return result;
        }

        static void OnPlanet(Vessel ship, int credits, Planet planet)
        {
            string[] gui = Program.GuiGenerate(ship, credits);

            Console.Clear();

            for (var i = 0; i < gui.Length; i++)
            {
                Console.WriteLine(gui[i]);
            }

            Console.Write("\n");

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
