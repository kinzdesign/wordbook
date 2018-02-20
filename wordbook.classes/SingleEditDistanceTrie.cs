using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        #region GetSingle[InDelSub]Peers

        public HashSet<int> GetSingleInsertionPeers(string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");
            if (word.Length != WordLength - 1)
                throw new ArgumentException("word.Length must be one less than Trie's WordLength");
            throw new NotImplementedException();

        }

        public HashSet<int> GetSingleDeletionPeers(string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");
            if (word.Length != WordLength + 1)
                throw new ArgumentException("word.Length must be one more than Trie's WordLength");
            throw new NotImplementedException();
        }

        public HashSet<int> GetSingleSubstitutionPeers(string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");
            if (word.Length != WordLength)
                throw new ArgumentException("word.Length must be equal to Trie's WordLength");

            // initialize peer list and processing stack
            HashSet<int> peers = new HashSet<int>();
            Stack<TraversalState> stack = new Stack<TraversalState>();
            // push root nodes onto stack
            foreach (var node in Children.Values)
                stack.Push(new TraversalState(0, node, false));
            // process nodes until stack is exhausted
            while (stack.Count > 0)
            {
                // pop next node to process
                var state = stack.Pop();
                // check whether match
                if (state.Node.Letter == word[state.CharIndex])
                {
                    // if word is finished
                    if (state.CharIndex + 1 == word.Length)
                    {
                        // if edited, add to peers
                        if (state.MadeEdit && state.Node.WordNumber.HasValue)
                            peers.Add(state.Node.WordNumber.Value);
                        // if no edit made, do nothing (edit distace to self is 0)
                    }
                    else
                    {
                        // push children
                        foreach (var child in state.Node.Children.Values)
                            stack.Push(new TraversalState(state.CharIndex + 1, child, state.MadeEdit));
                    }
                }
                // letter mismatch
                else
                {
                    // if mismatch and word is finished
                    if (state.CharIndex + 1 == word.Length)
                    {
                        // if edited, add to peers
                        if (!state.MadeEdit && state.Node.WordNumber.HasValue)
                            peers.Add(state.Node.WordNumber.Value);
                    }
                    else
                    {
                        // if mismatch and no edit made, push children
                        if (!state.MadeEdit)
                            foreach (var child in state.Node.Children.Values)
                                stack.Push(new TraversalState(state.CharIndex + 1, child, true));
                    }
                }
            }

            return peers;
        }

        #endregion
    }
}
