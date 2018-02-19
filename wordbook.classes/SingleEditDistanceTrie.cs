using System;

namespace wordbook
{
    public class SingleEditDistanceTrie : TrieNode
    {
        #region instance properties

        public readonly int WordLength;

        public int WordCount { get; private set; }

        #endregion

        #region constructor

        public SingleEditDistanceTrie(int wordLength) : base((char)0) // use null char as payload for root node
        {
            if (wordLength < 1)
                throw new ArgumentOutOfRangeException("length");
            WordLength = wordLength;
            WordCount = 0;
        }

        #endregion

        #region public methods

        public void Add(string word, int wordNumber)
        {
            if (word == null)
                throw new ArgumentNullException("word");
            if (word.Length != WordLength)
                throw new ArgumentException("word.Length must be equal to Trie's WordLength");
            // insert word into trie one letter at a time
            TrieNode node = this;
            for (int i = 0; i < word.Length; i++)
            {
                // get/create child node, if end of word, set wordNumber
                node = node.GetOrCreateChildNode(word[i], i + 1 == word.Length ? (int?)wordNumber : null);
            }
            // increment counter
            WordCount++;
        }

        public bool Contains(string word)
        {
            if (word == null || word.Length != WordLength)
                return false;
            // check for presence in Trie at each letter
            TrieNode node = this;
            for (int i = 0; i < word.Length; i++)
            {
                // get child node with appropriate letter
                node = node.GetChildNode(word[i]);
                // if not found, return false
                if (node == null)
                    return false;
            }
            // if made it this far, word is present
            return true;
        }

        #endregion
    }
}
