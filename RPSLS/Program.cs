using System;

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

            GameController gc = new(maxTurns);

            gc.PlayGame();

        }
    }
}
