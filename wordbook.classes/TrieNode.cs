using System.Collections.Generic;

namespace wordbook
{
    public class TrieNode
    {
        public readonly char Letter;

        public readonly int? WordNumber;

        private Dictionary<char, TrieNode> _children;

        public TrieNode(char letter, int? wordNumber = null)
        {
            Letter = letter;
            WordNumber = wordNumber;
            _children = new Dictionary<char, TrieNode>();
        }
    }
}
