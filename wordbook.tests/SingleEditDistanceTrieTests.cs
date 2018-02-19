using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordbook.tests
{
    [TestClass]
    public class SingleEditDistanceTrieTests
    {
        const string FOO = "FOO";

        #region Constructor

        [TestMethod]
        public void TestTrieConstructor()
        {
            int length = 5;
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(length);
            Assert.AreEqual(length, trie.WordLength, "Incorrect WordLength after calling constructor");
            Assert.AreEqual(0, trie.WordCount, "Non-zero Count after creation");
        }

        #endregion

        #region Add

        [TestMethod]
        public void TestTrieAdd()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.Add(FOO, 0);
            Assert.AreEqual(1, trie.WordCount, "Count should be 1 after adding");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestTrieAddNull()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.Add(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTrieAddWrongLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length - 1);
            trie.Add(FOO, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestTrieAddDuplicate()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.Add(FOO, 0);
            trie.Add(FOO, 1);
        }

        #endregion

        #region Contains

        [TestMethod]
        public void TestTrieContains()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            Assert.AreEqual(false, trie.Contains(FOO), "FOO was present before adding");
            trie.Add(FOO, 0);
            Assert.AreEqual(true, trie.Contains(FOO), "FOO was not present after adding");
            Assert.AreEqual(false, trie.Contains("FOB"), "FOB was present, but not added");
        }

        #endregion

        #region GetSingleInsertionPeers

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSingleInsertionPeersNull()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleInsertionPeers(null);
        }

        [TestMethod]
        public void TestGetSingleInsertionPeersCorrectLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length + 1);
            trie.GetSingleInsertionPeers(FOO);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetSingleInsertionPeersWrongLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleInsertionPeers(FOO);
        }

        #endregion

        #region GetSingleDeletionPeers

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSingleDeletionPeersNull()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleDeletionPeers(null);
        }

        [TestMethod]
        public void TestGetSingleDeletionPeersCorrectLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length - 1);
            trie.GetSingleDeletionPeers(FOO);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetSingleDeletionPeersWrongLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleDeletionPeers(FOO);
        }

        #endregion

        #region GetSingleSubstitutionPeers

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestGetSingleSubstitutionPeersNull()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleSubstitutionPeers(null);
        }

        [TestMethod]
        public void TestGetSingleSubstitutionPeersCorrectLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length);
            trie.GetSingleSubstitutionPeers(FOO);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetSingleSubstitutionPeersWrongLength()
        {
            SingleEditDistanceTrie trie = new SingleEditDistanceTrie(FOO.Length - 1);
            trie.GetSingleSubstitutionPeers(FOO);
        }

        #endregion
    }
}
