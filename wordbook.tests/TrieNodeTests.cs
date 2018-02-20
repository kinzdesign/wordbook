using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wordbook.tests
{
    [TestClass]
    public class TrieNodeTests
    {
        [TestMethod]
        public void TestTrieNodeWithNumber()
        {
            TestTrieNode('K', 1);
        }

        [TestMethod]
        public void TestTrieNodeWithoutNumber()
        {
            TestTrieNode('R');
        }

        private void TestTrieNode(char letter, int? wordNumber = null)
        {
            TrieNode node = new TrieNode(letter, wordNumber);
            Assert.AreEqual(letter, node.Letter);
            Assert.AreEqual(wordNumber, node.WordNumber);
        }
    }
}
