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

            Console.Clear();
            Program.OnPlanet(ship, 10000, new Earth());
        }

        static void GuiGenerate(Vessel ship, int credits)
        {
            string[] gui = new string[6];
            gui[0] = "Корабль: ";

            string shipClass = ship is Corvette ? "Корвет" : null;
            shipClass = ship is Frigate ? "Фрегат" : shipClass;
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
            gui[5] = "Основные опции (работают в любом диалоге): [s] — сохранить игру | [q] — выйти из игры";

            for (var i = 0; i < gui.Length; i++)
            {
                Console.WriteLine(gui[i]);
            }

            Console.Write("\n");
        }

        static void OnPlanet(Vessel ship, int credits, Planet planet)
        {
            Program.GuiGenerate(ship, credits);

            Console.WriteLine("Вы находитесь на планете "+PlanetParameters.PlanetName(planet, "ru")+". Куда бы вы хотели отправиться?");
            Console.WriteLine("[1] — Верфь");
            Console.WriteLine("[2] — Транспортная компания");
            Console.WriteLine("[3] — Биржа труда");
            Console.WriteLine("[4] — Космопорт (улететь отсюда)");
            Console.Write("\nВыберите один из вариантов: ");
            
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                Console.Clear();
                Program.OnShipyard(ship, credits, planet);
                break;

                default:
                break;
            }
        }

        static void OnShipyard(Vessel ship, int credits, Planet planet)
        {
            Program.GuiGenerate(ship, credits);

            Console.WriteLine("Вы перегнали свой корабль на верфь планеты "+PlanetParameters.PlanetName(planet, "ru")+".");
            Console.WriteLine("Работник вашего дока передал на ваш компьютер приветственное сообщение и запрос следующего действия.\n");
            Console.WriteLine("Что вы хотите сделать на верфи?");
            Console.WriteLine("[1] — Поменять корабль");
            Console.WriteLine("[2] — Установить новые орудия на корабль");
            Console.WriteLine("[3] — Ничего (покинуть верфь)");
            Console.Write("\nВыберите один из вариантов: ");
            
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "1":
                Console.Clear();
                Program.OnShipyardGetNewShip(ship, credits, planet);
                break;

                case "3":
                Console.Clear();
                Program.OnPlanet(ship, credits, planet);
                break;

                default:
                break;
            }
        }

        static void OnShipyardGetNewShip(Vessel ship, int credits, Planet planet)
        {
            Program.GuiGenerate(ship, credits);

            Console.WriteLine("Вы подключились к местному рынку подержанных кораблей.");
            Console.WriteLine("Да, вам доступны только подержанные корабли, ведь вы — всего лишь частное лицо.");
            Console.WriteLine("Следующие корабли доступны для покупки вместо вашего с доплатой разницы");
            Console.WriteLine("(стоимость вашего корабля в данный момент составляет "+Program.ShipCost(ship)+"):\n");

            int[] prices = new int[planet.SecondhandShips.Length];
            string[] answers = new string[planet.SecondhandShips.Length + 3];

            for (var i = 0; i < planet.SecondhandShips.Length; i++)
            {
                prices[i] = Program.ShipCost(planet.SecondhandShips[i]);
                answers[i] = $"{i + 1}";

                Console.WriteLine($"[{i + 1}] — \"{planet.SecondhandShips[i].Name}\", класс {planet.SecondhandShips[i].Size}, цена — {prices[i]}, здоровье — {planet.SecondhandShips[i].Health}/{planet.SecondhandShips[i].HealthMax}");
            }

            answers[planet.SecondhandShips.Length] = "0";
            answers[planet.SecondhandShips.Length + 1] = "s";
            answers[planet.SecondhandShips.Length + 2] = "q";

            Console.WriteLine("[0] — Вернуться назад");
            Console.Write("\nВыберите один из вариантов: ");

            string answer = Console.ReadLine();
            int answerIndex = Array.IndexOf(answers, answer);

            if ((answerIndex == (answers.GetLowerBound(0) - 1)) || (answer == "0"))
            {
                Console.Clear();
                Program.OnShipyard(ship, credits, planet);
            }
            else
            {
                if (answer == "s" || answer == "q")
                {}
                else
                {
                    if ((credits + Program.ShipCost(ship)) >= prices[answerIndex])
                    {
                        Console.Clear();
                        Console.WriteLine("\n|| Вы успешно обменяли корабль.\n");
                        Program.OnShipyard(planet.TradeShip(ship, answerIndex), (credits + Program.ShipCost(ship) - prices[answerIndex]), planet);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n|| Вам не хватает денег для обмена на данный корабль.\n");
                        Program.OnShipyardGetNewShip(ship, credits, planet);
                    }
                }
            }
        }

        static int ShipCost(Vessel ship)
        {
            return (10000 * ship.Size * 2 * ship.Health)/ship.HealthMax;
        }
    }
}
