using System;
using System.Collections.Generic;

namespace wordbook
{
    public class TrieNode
    {
        public readonly char Letter;

        public readonly int? WordNumber;

        protected Dictionary<char, TrieNode> _children;

        public TrieNode(char letter, int? wordNumber = null)
        {
            Letter = letter;
            WordNumber = wordNumber;
            _children = new Dictionary<char, TrieNode>();
        }

        internal TrieNode GetChildNode(char letter)
        {
            TrieNode node;
            if (!_children.TryGetValue(letter, out node))
                return null;
            return node;
        }

        internal TrieNode GetOrCreateChildNode(char letter, int? wordNumber = null)
        {
            // check whether the child node exists
            TrieNode node;
            if (!_children.TryGetValue(letter, out node))
            {
                // if not, create and add it
                node = new TrieNode(letter, wordNumber);
                _children.Add(letter, node);
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
