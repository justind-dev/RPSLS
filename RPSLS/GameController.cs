using System;
using System.Collections.Generic;
using System.Linq;

namespace RPSLS
{
    public class GameController
    {
        public int MaxTurns { get; set; } = 3;
        public int playerScore = 0;
        public int computerScore = 0;
        public int currentTurn = 0;
        public string gameWinner = "";
        public List<Move> computerMoves = new();
        public List<Move> moveDatabase = new();
        private Move lastMove = null;

        public GameController(int maxTurns)
        {
            if (maxTurns < 3)
            {
                MaxTurns = 3;
            }

            MaxTurns = maxTurns;

            InitializeGameData();
            lastMove = moveDatabase[0];
        }

        public void InitializeGameData()
        {
            // we need to generate our move types;
            var rock = new Move()
            {
                Name = "Rock"
            };
            var paper = new Move()
            {
                Name = "Paper"
            };
            var scissors = new Move()
            {
                Name = "Scissors"
            };
            var lizard = new Move()
            {
                Name = "Lizard"
            };
            var spock = new Move()
            {
                Name = "Spock"
            };

            //Configure weaknesses
            rock.AddWeakness(paper);
            rock.AddWeakness(spock);

            paper.AddWeakness(scissors);
            paper.AddWeakness(lizard);

            scissors.AddWeakness(rock);
            scissors.AddWeakness(spock);

            lizard.AddWeakness(scissors);
            lizard.AddWeakness(rock);

            spock.AddWeakness(lizard);
            spock.AddWeakness(paper);

            //set up moves database
            moveDatabase.Add(rock);
            moveDatabase.Add(paper);
            moveDatabase.Add(scissors);
            moveDatabase.Add(lizard);
            moveDatabase.Add(spock);
        }

        public Move GetComputerMove()
        {
            var selectRandomMove = new Random();

            int choice = selectRandomMove.Next(moveDatabase.Count);
            var move = moveDatabase[choice];

            //This prevents next move being the same as the last.
            while (move.Name == lastMove.Name)
            {
                choice = selectRandomMove.Next(moveDatabase.Count);
                move = moveDatabase[choice];
            }

            lastMove = move;
            return move;
        }

        public void PlayGame()
        {
            currentTurn = 1;

            //Begin Gameplay
            while (currentTurn <= this.MaxTurns)
            {
                Console.WriteLine($"Round {currentTurn}");
                var computerChoice = GetComputerMove();

                var playerChoice = GetPlayerMove();

                Console.WriteLine($"You chose: {playerChoice.Name}");

                System.Threading.Thread.Sleep(300);

                Console.WriteLine($"Computer Chose: {computerChoice.Name}");

                CheckMove(playerChoice, computerChoice);

            }

            EndGame();

        }

        public Move GetPlayerMove()
        {
            var goodChoice = false;
            IEnumerable<Move> moveChoice = null;
            Move playerMove = null;

            while (goodChoice == false)
            {
                Console.WriteLine("(rock, paper, scissors, lizard, spock)");
                Console.WriteLine("Choice:");

                try
                {
                    var choice = Console.ReadLine();
                    moveChoice = from move in moveDatabase
                                 where move.Name.ToLower() == choice.ToLower()
                                 select move;
                    if (moveChoice.Any())
                    {
                        goodChoice = true;
                        playerMove = moveChoice.First();
                    }
                    else
                    {
                        Console.WriteLine("Move name not found. Please try again.");
                        continue;
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Move name not found. Please try again.");
                    continue;
                }
            }
            return playerMove;

        }

        public void CheckMove(Move playerChoice, Move computerChoice)
        {
            if (playerChoice.Name == computerChoice.Name)
            {
                System.Threading.Thread.Sleep(300);
                Console.WriteLine($"Draw! {playerChoice.Name} is the same as {computerChoice.Name}");
                currentTurn++;
                Console.WriteLine("");
            }

            if (playerChoice.WeakTo(computerChoice))
            {
                System.Threading.Thread.Sleep(300);
                Console.WriteLine($"You lose this round! {computerChoice.Name} beats {playerChoice.Name}");
                computerScore += 1;
                currentTurn++;
                Console.WriteLine("");
            }
            else if (computerChoice.WeakTo(playerChoice))
            {
                System.Threading.Thread.Sleep(300);
                Console.WriteLine($"You win this round! {playerChoice.Name} beats {computerChoice.Name}");
                playerScore += 1;
                currentTurn++;
                Console.WriteLine("");
            }
            else
            {
                System.Threading.Thread.Sleep(300);
                Console.WriteLine($"Uncaught exception! {playerChoice.Name} versus {computerChoice.Name}");
                Console.WriteLine("");
            }
        }

        public void EndGame()
        {
            Console.WriteLine("END OF GAME RESULTS");

            Console.WriteLine($"Player Score: {playerScore} | Computer Score: {computerScore}");
            if (playerScore == computerScore)
            {
                Console.WriteLine("Its a draw!");
            }
            if (playerScore > computerScore)
            {
                Console.WriteLine("You win!");
            }
            if (playerScore < computerScore)
            {
                Console.WriteLine("You lose!");
            }
        }
    }
}
