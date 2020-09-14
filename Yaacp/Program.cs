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
            Console.WriteLine("Добро пожаловать в интерфейс управления бортовым компьютером. Это — его первый пользовательский запуск, ваш корабль в данный момент не имеет собственной сигнатуры.");
            Console.Write("Для генерации сигнатуры введите, пожалуйста, имя корабля: ");

            string shipName = Console.ReadLine();
            Vessel ship = new Corvette(shipName);
            ship.Armament = new Cannon[3];
            int credits = 10000;

            for (var i = 0; i < ship.Armament.Length; i++)
            {
                ship.Armament[i] = new Laser(1) {Load = true};
            }

            int troopsIndex = Array.IndexOf(ship.CrewTypes, "troops");
            int sailorsIndex = Array.IndexOf(ship.CrewTypes, "sailors");

            ship.Crew[troopsIndex] = 40;
            ship.Crew[sailorsIndex] = 30;

            Console.WriteLine($"\nВаш корабль — корвет по имени {ship.Name}. В данный момент он имеет {ship.Armament.Length} лазерных пушки и {ship.Crew[0] + ship.Crew[1]} человек экипажа — {ship.Crew[sailorsIndex]} матросов и {ship.Crew[troopsIndex]} солдат.");
            Console.ReadKey();
        }
    }
}
