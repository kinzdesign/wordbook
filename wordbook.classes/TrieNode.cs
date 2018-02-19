using System;
using System.Collections.Generic;

namespace wordbook
{
    public class TrieNode
    {
        public readonly char Letter;

        public readonly int? WordNumber;

        internal Dictionary<char, TrieNode> Children;

        public TrieNode(char letter, int? wordNumber = null)
        {
            Letter = letter;
            WordNumber = wordNumber;
            Children = new Dictionary<char, TrieNode>();
        }

        internal TrieNode GetChildNode(char letter)
        {
            TrieNode node;
            if (!Children.TryGetValue(letter, out node))
                return null;
            return node;
        }

        internal TrieNode GetOrCreateChildNode(char letter, int? wordNumber = null)
        {
            // check whether the child node exists
            TrieNode node;
            if (!Children.TryGetValue(letter, out node))
            {
                // if not, create and add it
                node = new TrieNode(letter, wordNumber);
                Children.Add(letter, node);
            }
            // throw error if duplicate word added
            else if (node.WordNumber.HasValue && wordNumber.HasValue)
            {
                throw new ArgumentException("Duplicate word added");
            }
            return node;
        }
    }
}
