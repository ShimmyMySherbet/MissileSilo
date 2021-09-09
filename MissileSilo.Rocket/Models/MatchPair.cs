namespace MissileSilo.Rocket.Models
{
    public struct MatchPair<T>
    {
        public T Source { get; private set; }
        public T Replacement { get; private set; }

        public MatchPair(T source, T replacement)
        {
            Source = source;
            Replacement = replacement;
        }
    }
}