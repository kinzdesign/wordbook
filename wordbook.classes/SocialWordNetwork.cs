﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace wordbook
{
    public class SocialWordNetwork
    {
        #region instance variables

        public Dictionaries.Keys Key { get; private set; }

        private string[] _words;
        public string[] Words
        {
            get
            {
                // lazy-load dictionary on first use
                if (_words == null)
                    LoadDictionary();
                return _words;
            }
        }

        private Dictionary<int, SingleEditDistanceTrie> _triesByLength;
        public Dictionary<int, SingleEditDistanceTrie> TriesByLength
        {
            get
            {
                // lazy-load tries on first use
                if (_triesByLength == null)
                    BuildNetwork();
                return _triesByLength;
            }
        }

        private DictionarySet<int, int> _friendships;
        public DictionarySet<int, int> Friendships
        {
            get
            {
                // lazy-load friendships on first use
                if (_friendships == null)
                    BuildNetwork();
                return _friendships;
            }
        }

        #endregion

        #region elapsed time tracking

        private DateTime? BeganLoadDictionary;
        private DateTime? EndedLoadDictionary;
        public TimeSpan? ElapsedLoadDictionary
        {
            get
            {
                if (BeganLoadDictionary.HasValue && EndedLoadDictionary.HasValue)
                    return EndedLoadDictionary.Value - BeganLoadDictionary.Value;
                return null;
            }
        }

        private DateTime? BeganBuildNetwork;
        private DateTime? EndedBuildNetwork;
        public TimeSpan? ElapsedBuildNetwork
        {
            get
            {
                if (BeganBuildNetwork.HasValue && EndedBuildNetwork.HasValue)
                    return EndedBuildNetwork.Value - BeganBuildNetwork.Value;
                return null;
            }
        }

        private DateTime? BeganGetExtendedNetwork;
        private DateTime? EndedGetExtendedNetwork;
        public TimeSpan? ElapsedGetExtendedNetwork
        {
            get
            {
                if (BeganGetExtendedNetwork.HasValue && EndedGetExtendedNetwork.HasValue)
                    return EndedGetExtendedNetwork.Value - BeganGetExtendedNetwork.Value;
                return null;
            }
        }


        #endregion

        #region constructors

        public SocialWordNetwork(Dictionaries.Keys key)
        {
            // record the key
            Key = key;
        }

        #endregion

        #region load dictionary

        private void LoadDictionary()
        {
            BeganLoadDictionary = DateTime.Now;
            _words = Dictionaries.Get(Key);
            EndedLoadDictionary = DateTime.Now;
        }

        #endregion

        #region build network

        private void BuildNetwork()
        {
            BeganBuildNetwork = DateTime.Now;
            // instantiate collections
            _triesByLength = new Dictionary<int, SingleEditDistanceTrie>();
            _friendships = new DictionarySet<int, int>();
            // add words
            for (int i = 0; i < Words.Length; i++)
                AddWord(Words[i], i);
            EndedBuildNetwork = DateTime.Now;
        }

        private void AddWord(string word, int wordNumber)
        {
            // get length
            int length = word.Length;
            // ty to find proper Trie
            SingleEditDistanceTrie trie;
            if (!_triesByLength.TryGetValue(length, out trie))
            {
                // if Trie not exist, create it
                trie = new SingleEditDistanceTrie(length);
                _triesByLength.Add(length, trie);
            }
            AddPeers(word, wordNumber);
            // add to Trie
            trie.Add(word, wordNumber);
        }

        private void AddPeers(string word, int wordNumber)
        {
            int length = word.Length;
            SingleEditDistanceTrie trie;
            HashSet<int> peers;

            // get insertions
            if (TriesByLength.TryGetValue(length + 1, out trie))
            {
                peers = trie.GetSingleInsertionPeers(word);
                AddFriendships(wordNumber, peers);
            }

            // get deletions
            if (TriesByLength.TryGetValue(length - 1, out trie))
            {
                peers = trie.GetSingleDeletionPeers(word);
                AddFriendships(wordNumber, peers);
            }

            // get substitutions
            if (TriesByLength.TryGetValue(length, out trie))
            {
                peers = trie.GetSingleSubstitutionPeers(word);
                AddFriendships(wordNumber, peers);
            }
        }

        private void AddFriendships(int from, IEnumerable<int> to)
        {
            if (to != null)
            {
                // iterate all IDs in to
                foreach (int t in to)
                {
                    // add in both directions
                    _friendships.Add(from, t);
                    _friendships.Add(t, from);
                }
            }
        }

        #endregion

        #region extended network

        public int[] GetExtendedNetwork(string word)
        {
            if (word == null)
                throw new ArgumentNullException("word");
            BeganGetExtendedNetwork = DateTime.Now;
            // search for word in Words array
            int wordId = Array.BinarySearch(Words, word);
            // if not found in Words, it has no social network
            if (wordId < 0)
                return new int[] { };
            EndedGetExtendedNetwork = DateTime.Now;
            return GetExtendedNetwork(wordId);
        }

        public int[] GetExtendedNetwork(int wordId)
        {
            // check wordId is valid
            if (wordId < 0 || wordId >= Words.Length)
                throw new ArgumentOutOfRangeException("wordId");
            // create processing queue for BFS
            Queue<int> queue = new Queue<int>();
            // create set to hold social network
            HashSet<int> extendedNetwork = new HashSet<int>();
            // placeholder for temporary peer lists
            HashSet<int> tmpPeers;
            // enqueue self
            queue.Enqueue(wordId);
            // iterate until queue is exhausted
            while (queue.Count > 0)
            {
                // get next word to process
                wordId = queue.Dequeue();
                // add to extended network
                extendedNetwork.Add(wordId);
                // get friends of this word
                if (Friendships.TryGetValues(wordId, out tmpPeers))
                {
                    // iterate friends of this word
                    foreach (int peer in tmpPeers)
                    {
                        // if not yet in extended network
                        if (!extendedNetwork.Contains(peer))
                        {
                            // enqueue
                            queue.Enqueue(peer);
                        }
                    }
                }
            }
            // return network as array
            return extendedNetwork.ToArray();
        }

        #endregion
    }
}
