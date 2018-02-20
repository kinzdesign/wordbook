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
             SocialWordNetwork network = new SocialWordNetwork(Dictionaries.Keys.very_small_test_dictionary);

            TestFriendships(network);
            TestNetworkSize(network);
        }

        private void TestFriendships(SocialWordNetwork network)
        {
            // check friendships count
            Assert.AreEqual(12, network.Friendships.ValueCount, "Friendship count mismatch");

            // check actual friendships
            CheckFriendship(network, 0,  1);
            CheckFriendship(network, 1,  2);
            CheckFriendship(network, 2,  3);
            CheckFriendship(network, 3, 11);
            CheckFriendship(network, 5,  8);
            CheckFriendship(network, 9, 10);
        }

        private void CheckFriendship(SocialWordNetwork network, int a, int b)
        {
            string valA = network.Words[a];
            string valB = network.Words[b];
            Assert.IsTrue(network.Friendships[a, b], string.Format("{0} ({1}) should be friends with {2} ({3})", a, valA, b, valB));
        }

        private void TestNetworkSize(SocialWordNetwork network)
        {
            TestNetworkSize(network,  0, 5); // FIST     { 0, 1, 2, 3, 11 }
            TestNetworkSize(network,  1, 5); // FISTS    { 0, 1, 2, 3, 11 }
            TestNetworkSize(network,  2, 5); // LISTS    { 0, 1, 2, 3, 11 }
            TestNetworkSize(network,  3, 5); // LISTY    { 0, 1, 2, 3, 11 }
            TestNetworkSize(network,  4, 1); // LIT      { 4 }
            TestNetworkSize(network,  5, 2); // LITAI    { 5, 8 }
            TestNetworkSize(network,  6, 1); // LITANIES { 6 }
            TestNetworkSize(network,  7, 1); // LITANY   { 7 }
            TestNetworkSize(network,  8, 2); // LITAS    { 5, 8 }
            TestNetworkSize(network,  9, 2); // LITCHI   { 9, 10 }
            TestNetworkSize(network, 10, 2); // LITCHIS  { 9, 10 }
            TestNetworkSize(network, 11, 5); // LUSTY    { 0, 1, 2, 3, 11 }
        }

        private void TestNetworkSize(SocialWordNetwork network, int wordId, int expectedSize)
        {
            Assert.AreEqual(expectedSize, network.GetExtendedNetwork(wordId)?.Length ?? 0, "Incorrect extended network size for " + network.Words[wordId]);
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
        public void TestDeletionsWithFiveCharTrie()
        {
            SingleEditDistanceTrie trie = GetFiveCharTrie();
            HashSet<int> peers;

            // test deletion at end
            peers = trie.GetSingleDeletionPeers("LISTSS");
            Assert.AreEqual(1, peers.Count, "LISTSS should have 1 single-deletion peer in the 5-char trie");
            Assert.IsTrue(peers.Contains(2), "LISTS should be a single-deletion peer of LISTSS in the 5-char tree");

            // test deletion at beginning
            peers = trie.GetSingleDeletionPeers("LIUSTY");
            Assert.AreEqual(2, peers.Count, "LIUSTY should have 2 single-deletion peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(3), "LISTY should be a single-deletion peer of LIUSTY in the 5-char tree");
            Assert.IsTrue(peers.Contains(11), "LUSTY should be a single-deletion peer of LIUSTY in the 5-char tree");

            // test deletion in middle
            peers = trie.GetSingleDeletionPeers("FLISTS");
            Assert.AreEqual(2, peers.Count, "FLISTS should have 2 single-deletion peers in the 5-char trie");
            Assert.IsTrue(peers.Contains(1), "FISTS should be a single-deletion peer of FLISTS in the 5-char tree");
            Assert.IsTrue(peers.Contains(2), "LISTS should be a single-deletion peer of FLISTS in the 5-char tree");
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
