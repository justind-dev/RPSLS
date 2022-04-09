using System;
using System.Linq;

namespace RPSLS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- ROCK, PAPER, SCISSORS, LIZARD, SPOCK ----");
            Console.WriteLine("How many turns will we be playing for? (1-10) or 99 to exit");
            var maxTurns = 0;
            var value = 0;
            while (maxTurns == 0)
            {
                value = 0;
                try
                {
                    Console.WriteLine("Number of turns: ");
                    value = int.Parse(Console.ReadLine());
                    maxTurns = value;
                    Console.WriteLine($"Turns set to {value}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please input a valid number.");
                    continue;
                }
                if (value == 99)
                {
                    Console.WriteLine("Thanks for playing!");
                    Environment.Exit(0);
                }
                if (value < 0 || value > 10)
                {
                    Console.WriteLine("Please input a valid number between 1 and 10.");
                    continue;
                }

            }

            GameController gc = new GameController(maxTurns);

            var currentTurn = 1;

            //Begin Gameplay
            while (currentTurn <= maxTurns)
            {
                Console.WriteLine($"Round {currentTurn} - Please enter your move choice.");
                Console.WriteLine("(rock, paper, scissors, lizard, spock)");
                Console.WriteLine("Choice:");
                var choice = Console.ReadLine();
                try
                {
                    var moveChoice = from move in gc.moveDatabase
                                     where move.Name.ToLower() == choice.ToLower()
                                     select move;
                    var computerChoice = gc.GetComputerMove();

                    var playerChoice = moveChoice.FirstOrDefault();

                    Console.WriteLine($"You chose: {playerChoice.Name}");
                    System.Threading.Thread.Sleep(300);
                    Console.WriteLine($"Computer Chose: {computerChoice.Name}");

                    if (playerChoice.Name == computerChoice.Name)
                    {
                        System.Threading.Thread.Sleep(300);
                        Console.WriteLine($"Draw! {playerChoice.Name} is the same as {computerChoice.Name}");
                        currentTurn++;
                        Console.WriteLine("");
                        continue;
                    }

                    if (playerChoice.WeakTo(computerChoice))
                    {
                        System.Threading.Thread.Sleep(300);
                        Console.WriteLine($"You lose this round! {computerChoice.Name} beats {playerChoice.Name}");
                        gc.computerScore += 1;
                        currentTurn++;
                        Console.WriteLine("");
                        continue;
                    }
                    else if (computerChoice.WeakTo(playerChoice))
                    {
                        System.Threading.Thread.Sleep(300);
                        Console.WriteLine($"You win this round! {playerChoice.Name} beats {computerChoice.Name}");
                        gc.playerScore += 1;
                        currentTurn++;
                        Console.WriteLine("");
                        continue;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(300);
                        Console.WriteLine($"Uncaught exception! {playerChoice.Name} versus {computerChoice.Name}");
                        Console.WriteLine("");
                        continue;
                    }
                    {

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Move name not found. Please try again.");
                    continue;
                }
            }

            System.Threading.Thread.Sleep(300);
            Console.WriteLine("END OF GAME RESULTS");


            Console.WriteLine($"Player Score: {gc.playerScore} | Computer Score: {gc.computerScore}");
            if (gc.playerScore == gc.computerScore)
            {
                Console.WriteLine("Its a draw!");
            }
            if (gc.playerScore > gc.computerScore)
            {
                Console.WriteLine("You win!");
            }
            if (gc.playerScore < gc.computerScore)
            {
                Console.WriteLine("You lose!");
            }
        }
    }
}
