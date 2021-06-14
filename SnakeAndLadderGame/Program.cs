using System;
using System.Collections.Generic;

namespace Games
{
    class SnakeAndLadder
    {
        #region game attributes
        static string[] playerNames = null;
        static int[] playerScores = null;
        static Dictionary<int, int> snakes = null;
        static Dictionary<int, int> ladders = null;
        static int numberOfPlayers = 2;
        static int maxGameScore = 100;
        #endregion

        static void Main(string[] args)
        {
            initializeSnakesAndLaddersPositioning();
            initializeGame(numberOfPlayers);
            printGameScores();
            Console.ReadKey();
        }

        #region game logic
        /// <summary>
        /// This function is used to 
        /// initialize updated positions of
        /// a user on encountering a ladder or a snake
        /// </summary>
        static void initializeSnakesAndLaddersPositioning()
        {
            snakes = new Dictionary<int, int>();
            snakes.Add(98, 5);
            snakes.Add(87, 53);
            snakes.Add(66, 10);
            snakes.Add(55, 9);

            ladders = new Dictionary<int, int>();
            ladders.Add(2, 25);
            ladders.Add(31, 52);
            ladders.Add(45, 63);
            ladders.Add(15, 95);
        }

        /// <summary>
        /// This takes into account number of players 
        /// in a game and handling of player scores
        /// after a successive win
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        static void initializeGame(int numberOfPlayers)
        {
            bool firstTimeRoll = true;
            int chanceCount = 1;
            while (true)
            {
                bool anyPlayerWins = rollDice(chanceCount, numberOfPlayers, firstTimeRoll);
                if (anyPlayerWins) break;
                firstTimeRoll = false;
                chanceCount++;
            }

        }

        /// <summary>
        /// This takes onto account related order
        /// of chances for every player on the basis of player count.
        /// This will also initialize the related player arrays for scores and names
        /// </summary>
        /// <param name="chanceCount"></param>
        /// <param name="numberOfPlayers"></param>
        /// <param name="firstTimeRoll"></param>
        /// <returns></returns>
        static bool rollDice(int chanceCount, int numberOfPlayers, bool firstTimeRoll = false)
        {
            Console.WriteLine(string.Concat(Environment.NewLine, " =>Chance Count ", chanceCount));
            bool anyPlayerWins = false;
            if (firstTimeRoll)
            {
                playerNames = new string[numberOfPlayers];
                playerScores = new int[numberOfPlayers];
            }
            for (int playerOrder = 0; playerOrder < numberOfPlayers; playerOrder++)
            {
                if (firstTimeRoll)
                {
                    Console.WriteLine("Enter name of player " + (playerOrder + 1));
                    playerNames[playerOrder] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(string.Format("{0} - Please press enter to roll a dice  ", playerNames[playerOrder]));
                    //Console.ReadKey();
                    Random rnd = new Random();
                    int dice = rnd.Next(1, 7);   // creates a dice roll between 1 and 6
                    Console.WriteLine("Diced Value Is " + dice);
                    rolledDicedMoveLogic(dice, playerOrder);
                    if (playerScores[playerOrder] == 100) { anyPlayerWins = true; return anyPlayerWins; }
                }
            }
            return anyPlayerWins;
        }

        /// <summary>
        /// On the basis of dice score, this updates
        /// the player scores(taking onto account if the dice score encounter a position 
        /// related to ladder move or a snake move or a normal move)
        /// </summary>
        /// <param name="diceScore"></param>
        /// <param name="playerOrder"></param>
        static void rolledDicedMoveLogic(int diceScore, int playerOrder)
        {
            int sumOfScores = playerScores[playerOrder] + diceScore;
            if (sumOfScores <= maxGameScore)
                playerScores[playerOrder] = sumOfScores;
            int updatedDiceScore = playerScores[playerOrder];
            bool searchScoreInSnakesArray = snakes.ContainsKey(updatedDiceScore);
            bool searchScoreInLaddersArray = ladders.ContainsKey(updatedDiceScore);
            if (searchScoreInSnakesArray)
            {
                playerScores[playerOrder] = snakes[updatedDiceScore];
                Console.WriteLine(string.Format("Dice score for player {0} was and updated score was {1} and resultant score(due to snake) was {2}", diceScore, updatedDiceScore, playerScores[playerOrder]));
            }
            else if (searchScoreInLaddersArray)
            {
                playerScores[playerOrder] = ladders[updatedDiceScore];
                Console.WriteLine(string.Format("Dice score for player was {0} and updated score was {1} and resultant score(due to ladder) was {2}", diceScore, updatedDiceScore, playerScores[playerOrder]));
            }
            else
                Console.WriteLine(string.Format("Dice score for player was {0} and resultant score was {1}", diceScore, playerScores[playerOrder]));

        }
        #endregion

        #region game results
        static void printGameScores()
        {
            Console.WriteLine(string.Concat(Environment.NewLine, " Snake And Ladder Game Results"));
            for (int playerNumber = 0; playerNumber < numberOfPlayers; playerNumber++)
            {
                Console.WriteLine(string.Format("Score for {0} is {1}", playerNames[playerNumber], playerScores[playerNumber]));
            }
        }
        #endregion
    }
}