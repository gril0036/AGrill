using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using BattleShip.BLL.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BattleShip.UI
{
    public class UserIO
    {



        public static void SplashScreen()
        {
            Console.WriteLine();
            Console.WriteLine("\t ======================");
            Console.WriteLine("\t Welcome to Battleship!");
            Console.WriteLine("\t ======================");
            Console.WriteLine();
            Console.WriteLine("\tPress enter key to Start!");
            Console.ReadLine();
            Console.Clear();

        }



        public static void setUpPlayer(Player currentPlayer)
        {
            currentPlayer.Name = GetStringFromUser("Please Enter Your Name:");
            Console.Clear();

        }

        public static void destroyerPlacement(Player player)
        {
            while (true)
            {
                Console.WriteLine($"{player.Name} Place your Destroyer");
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Destroyer;
                request.Coordinate = getPlayerCoord();
                request.Direction = getPlayerShipDirection();

                ShipPlacement response = new ShipPlacement();
                response = player.PlayerBoard.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space, try again");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ship overlap, try again");
                        continue;
                    case ShipPlacement.Ok:
                        break;
                }
                Console.Clear();
                break;
            }
        }

        public static void submarinePlacement(Player player)
        {
            while (true)
            {
                Console.WriteLine($"{player.Name} Place your Submarine");
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Submarine;
                request.Coordinate = getPlayerCoord();
                request.Direction = getPlayerShipDirection();

                ShipPlacement response = new ShipPlacement();
                response = player.PlayerBoard.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space, try again");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ship overlap, try again");
                        continue;
                    case ShipPlacement.Ok:
                        break;
                }
                Console.Clear();
                break;
            }
        }

        public static void cruiserPlacement(Player player)
        {
            while (true)
            {
                Console.WriteLine($"{player.Name} Place your Cruiser");
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Cruiser;
                request.Coordinate = getPlayerCoord();
                request.Direction = getPlayerShipDirection();

                ShipPlacement response = new ShipPlacement();
                response = player.PlayerBoard.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space, try again");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ship overlap, try again");
                        continue;
                    case ShipPlacement.Ok:
                        break;
                }
                Console.Clear();
                break;
            }
        }

        public static void battleshipPlacement(Player player)
        {
            while (true)
            {
                Console.WriteLine($"{player.Name} Place your Battleship");
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Battleship;
                request.Coordinate = getPlayerCoord();
                request.Direction = getPlayerShipDirection();

                ShipPlacement response = new ShipPlacement();
                response = player.PlayerBoard.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space, try again");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ship overlap, try again");
                        continue;
                    case ShipPlacement.Ok:
                        break;
                }
                Console.Clear();
                break;
            }
        }

        public static void carrierPlacement(Player player)
        {
            while (true)
            {
                Console.WriteLine($"{player.Name} Place your Carrier");
                PlaceShipRequest request = new PlaceShipRequest();
                request.ShipType = ShipType.Carrier;
                request.Coordinate = getPlayerCoord();
                request.Direction = getPlayerShipDirection();

                ShipPlacement response = new ShipPlacement();
                response = player.PlayerBoard.PlaceShip(request);

                switch (response)
                {
                    case ShipPlacement.NotEnoughSpace:
                        Console.WriteLine("Not enough space, try again");
                        continue;
                    case ShipPlacement.Overlap:
                        Console.WriteLine("Ship overlap, try again");
                        continue;
                    case ShipPlacement.Ok:
                        break;
                }
                Console.Clear();
                break;
            }
        }


        public static ShipDirection getPlayerShipDirection()
        {
            while (true)
            {
                string direction = "";

                direction = GetStringFromUser("What direction would you like? (Up, Down, Left, Right)");

                switch (direction.ToUpper())
                {
                    case "UP":
                        return ShipDirection.Up;
                    case "DOWN":
                        return ShipDirection.Down;
                    case "LEFT":
                        return ShipDirection.Left;
                    case "RIGHT":
                        return ShipDirection.Right;
                }
            }
        }

        public static void twoPlayerGame(Player player1, Player player2)
        {
            bool victory = false;
            Player currentPlayer = player1;
            Player otherPlayer = player2;
            Coordinate coord;
            FireShotResponse response = new FireShotResponse();

            while (victory == false)
            {

                do
                {
                    DisplayBoard(otherPlayer);
                    Console.WriteLine($"{currentPlayer.Name} What are your shot coordinates?");
                    coord = getPlayerCoord();

                    response = otherPlayer.PlayerBoard.FireShot(coord);

                    displayShotStatus(otherPlayer, response);

                } while (response.ShotStatus == ShotStatus.Duplicate || response.ShotStatus == ShotStatus.Invalid);

                if (response.ShotStatus == ShotStatus.Victory)
                {
                    victory = true;
                    Console.WriteLine("You win! Press enter to end the game!");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                }

                else if (response.ShotStatus == ShotStatus.Hit || response.ShotStatus == ShotStatus.HitAndSunk || response.ShotStatus == ShotStatus.Miss)
                {
                    Player tempPlayer = currentPlayer;
                    currentPlayer = otherPlayer;
                    otherPlayer = tempPlayer;
                    Console.Clear();
                }
            }
        }

        private static void displayShotStatus(Player otherPlayer, FireShotResponse response)
        {
            switch (response.ShotStatus)
            {
                case ShotStatus.Invalid:
                    Console.WriteLine("Invalid shot location, try again.");
                    break;
                case ShotStatus.Duplicate:
                    Console.WriteLine("Duplicate shot locaion, try again.");
                    break;
                case ShotStatus.Miss:
                    Console.WriteLine("Miss!");
                    break;
                case ShotStatus.Hit:
                    Console.WriteLine("Hit!");
                    break;
                case ShotStatus.HitAndSunk:
                    Console.WriteLine($"Hit and sunk {otherPlayer.Name}'s {response.ShipImpacted}!");
                    break;
                case ShotStatus.Victory:
                    Console.WriteLine("You win!");
                    break;
            }
            Console.WriteLine("Press enter to end your turn.");
            Console.ReadKey();
            Console.Clear();
        }

        public static Coordinate getPlayerCoord()
        {

            while (true)
            {
                string coords = GetStringFromUser("Please enter coordinates (A1 format): ");
                //set up validation of coords entry
                coords = coords.ToUpper();
                string xStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                char xChar = coords[0];
                int XCoordinate = 0;
                int YCoordinate = 0;

                if (char.IsLetter(xChar) == true)
                {
                    XCoordinate = xStr.IndexOf(coords.Substring(0, 1)) + 1;
                    if (XCoordinate > 10 || XCoordinate < 1)
                    {
                        continue;
                    }
                }

                YCoordinate = int.Parse(coords.Substring(1));
                if (YCoordinate > 10 || YCoordinate < 1)
                {
                    continue;
                }

                Coordinate playerCoord = new Coordinate(XCoordinate, YCoordinate);

                return playerCoord;
            }
        }

        public static string GetStringFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string result = Console.ReadLine();
                if (result == "" || result == string.Empty)
                {
                    Console.WriteLine("You entered a blank, please try again");
                    continue;
                }
                return result;
            }
        }

        public static void DisplayBoard(Player player)
        {
            string xStr = "ABCDEFGHIJ";
            char row = 'Z';
            ShotHistory displayShot = new ShotHistory();
            Console.Write("   [1  2  3  4  5  6  7  8  9 10]");
            for (int x = 0; x < 10; x++)
            {
                row = xStr[x];
                Console.Write($"\n[{row}]");
                for (int y = 0; y < 10; y++)
                {
                    Coordinate coord = new Coordinate(x + 1, y + 1);
                    displayShot = player.PlayerBoard.CheckCoordinate(coord);
                    if (displayShot == ShotHistory.Unknown)
                    {
                        Console.Write($"[ ]");
                    }
                    if (displayShot == ShotHistory.Hit)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[H]");
                    }
                    if (displayShot == ShotHistory.Miss)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("[M]");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.WriteLine();
        }
    }
}
