using System.Collections.Generic;
using System.Linq;

namespace RPSLS
{
    public class Move : IMove<Move>
    {
        public string? Name { get; set; }

        public List<Move> WeakAgainst { get; set; }

        public Move()
            {
            WeakAgainst = new List<Move>();
            }

        public bool WeakTo(Move otherMove)
        {
            if (otherMove == null)
            {
                return false;
            }
            var weaknesses = from move in WeakAgainst
                                  where move.Name.ToLower() == otherMove.Name.ToLower()
                                  select move;

            return weaknesses.Any();
        }

        public void AddWeakness(Move move)
        {
            WeakAgainst.Add(move);
        }

    }
}
