using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    public class GameWorkflow
    {
        public void Run()
        {

            do
            {
                UserIO.SplashScreen();

                Player player1 = new Player();
                Player player2 = new Player();

                UserIO.setUpPlayer(player1);

                UserIO.destroyerPlacement(player1);

                UserIO.submarinePlacement(player1);

                UserIO.cruiserPlacement(player1);

                UserIO.battleshipPlacement(player1);

                UserIO.carrierPlacement(player1);


                UserIO.setUpPlayer(player2);

                UserIO.destroyerPlacement(player2);

                UserIO.submarinePlacement(player2);

                UserIO.cruiserPlacement(player2);

                UserIO.battleshipPlacement(player2);

                UserIO.carrierPlacement(player2);


                UserIO.twoPlayerGame(player1, player2);

                bool play = false;

                do
                {

                    string playAgain = "";

                    Console.WriteLine("Would you like to play again? (1 = Yes, 2 = No)");
                    playAgain = Console.ReadLine();

                    if (playAgain == "2")
                    {
                        Console.WriteLine("Thanks for playing!");
                        Console.WriteLine("Press enter to exit");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    else if (playAgain == "1")
                    {
                        Console.Clear();
                        play = true;
                    }
                } while (play == false);

            } while (true);

        }
    }
}
