namespace wordbook
{
    internal struct TraversalState
    {
        internal readonly int CharIndex;
        internal readonly TrieNode Node;
        internal readonly bool MadeEdit;

        internal TraversalState(int charIndex, TrieNode node, bool madeEdit)
        {
            CharIndex = charIndex;
            Node = node;
            MadeEdit = madeEdit;
        }

        public override string ToString()
        {
            return string.Format("{0} at pos {1} - {2}edit made", Node.Letter, CharIndex, MadeEdit ? string.Empty : "NO ");
        }
    }
}
