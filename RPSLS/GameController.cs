using System;
using System.Collections.Generic;

namespace RPSLS
{
    public class GameController
    {
        public int MaxTurns { get; set; } = 3;
        public int playerScore = 0;
        public int computerScore = 0;
        public List<Move> computerMoves = new List<Move>();
        public List<Move> moveDatabase = new List<Move>();
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

    }
}
