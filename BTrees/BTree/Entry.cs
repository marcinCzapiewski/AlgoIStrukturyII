namespace BTree
{
    using System;

    public class Entry<TK> : IEquatable<Entry<TK>>
    {
        public TK Key { get; set; }

        public bool Equals(Entry<TK> other)
        {
            return Key.Equals(other.Key);
        }
    }
}
