using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace wordbook.tests
{
    /// <summary>
    /// Contains a number of tests based on computations done by hand on the very_small_test_dictionary
    /// </summary>
    [TestClass]
    public class VerySmallTestDictionaryManualTests
    {
        [TestMethod]
        public void RunVerySmallDictionaryTests()
        {
            // create network
            // SocialWordNetwork network = new SocialWordNetwork(Dictionaries.Keys.very_small_test_dictionary);

            // TestFriendshipCount(network, 12);
        }

        private void TestFriendshipCount(SocialWordNetwork network, int expectedCount)
        {
            // check friendships count
            int actualCount = network.Friendships?.ValueCount ?? 0;
            Assert.AreEqual(expectedCount, actualCount, "Friendship count mismatch");
        }

        private SingleEditDistanceTrie GetFiveCharTrie()
        {
            // populate five-char trie
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(5);
            trie.Add("FISTS", 1);
            trie.Add("LISTS", 2);
            trie.Add("LISTY", 3);
            trie.Add("LITAI", 5);
            trie.Add("LITAS", 8);
            trie.Add("LUSTY", 11);
            // check count
            Assert.AreEqual(6, trie.WordCount);
            return trie;
        }

        [TestMethod]
        public void TestInsertionsWithFiveCharTrie()
        {
            SingleEditDistanceTrie trie = GetFiveCharTrie();
            HashSet<int> peers;

            // test insertion at end
            peers = trie.GetSingleInsertionPeers("FIST");
            Assert.AreEqual(1, peers.Count, "FIST should have 1 single-insertion peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(1), "FISTS should be a single-insertion peer of FISTS in the 5-char tree");

            // test insertion at beginning
            peers = trie.GetSingleInsertionPeers("ISTS");
            Assert.AreEqual(2, peers.Count, "ISTS should have 2 single-insertion peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(1), "FISTS should be a single-insertion peer of ISTS in the 5-char tree");
            Assert.IsTrue(peers.Contains(2), "LISTS should be a single-insertion peer of ISTS in the 5-char tree");

            // test insertion in middle
            peers = trie.GetSingleInsertionPeers("LSTY");
            Assert.AreEqual(2, peers.Count, "LSTY should have 2 single-insertion peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(3), "LISTY should be a single-insertion peer of LSTY in the 5-char tree");
            Assert.IsTrue(peers.Contains(11), "LUSTY should be a single-insertion peer of LSTY in the 5-char tree");
        }

        [TestMethod]
        public void TestSubstitutionsWithFiveCharTrie()
        {
            SingleEditDistanceTrie trie = GetFiveCharTrie();
            HashSet<int> peers;

            peers = trie.GetSingleSubstitutionPeers("FISTS");
            Assert.AreEqual(1, peers.Count, "FISTS should have 1 single-substitution peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(2), "LISTS should be a single-substitution peer of FISTS in the 5-char tree");

            peers = trie.GetSingleSubstitutionPeers("LISTS");
            Assert.AreEqual(2, peers.Count, "LISTS should have 2 single-substitutions peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(1), "FISTS should be a single-substitution peer of LISTS in the 5-char tree");
            Assert.IsTrue(peers.Contains(3), "LISTY should be a single-substitution peer of LISTS in the 5-char tree");

            peers = trie.GetSingleSubstitutionPeers("LISTY");
            Assert.AreEqual(2, peers.Count, "LISTY should have 2 single-substitution peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(2), "LISTS should be a single-substitution peer of LISTY in the 5-char tree");
            Assert.IsTrue(peers.Contains(11), "LUSTY should be a single-substitution peer of LISTY in the 5-char tree");

            peers = trie.GetSingleSubstitutionPeers("LITAI");
            Assert.AreEqual(1, peers.Count, "LITAI should have 1 single-substitution peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(8), "LITAS should be a single-substitution peer of LITAI in the 5-char tree");

            peers = trie.GetSingleSubstitutionPeers("LITAS");
            Assert.AreEqual(1, peers.Count, "LITAS should have 1 single-substitution peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(5), "LITAI should be a single-substitution peer of LITAS in the 5-char tree");

            peers = trie.GetSingleSubstitutionPeers("LUSTY");
            Assert.AreEqual(1, peers.Count, "LUSTY should have 1 single-substitution peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(3), "LISTY should be a single-substitution peer of LUSTY in the 5-char tree");
        }
    }
}
