using System.Collections.Generic;

namespace RPSLS
{
    public interface IMove<T>
    {
        public string? Name { get; set; }
        public List<T> WeakAgainst { get; set; }

        bool WeakTo(T obj);
        void AddWeakness(T obj);

    }
}
