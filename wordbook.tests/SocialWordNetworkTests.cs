using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace wordbook.tests
{
    [TestClass]
    public class SocialWordNetworkTests
    {
        [TestMethod]
        public void TestNetworks()
        {
            TestNetwork(Dictionaries.Keys.dictionary);
            TestNetwork(Dictionaries.Keys.eight_dictionary);
            TestNetwork(Dictionaries.Keys.half_dictionary);
            TestNetwork(Dictionaries.Keys.quarter_dictionary);
            TestNetwork(Dictionaries.Keys.very_small_test_dictionary);
        }

        private void TestNetwork(Dictionaries.Keys key)
        {
            SocialWordNetwork network = TestConstructor(key);
            TestWordCount(network);
            if (key == Dictionaries.Keys.very_small_test_dictionary)
            {
                TestFriendshipCount(network, 12); // computed this value by hand, on paper
            }
        }

        private SocialWordNetwork TestConstructor(Dictionaries.Keys key)
        {
            SocialWordNetwork network = new SocialWordNetwork(key);
            // check keys match
            Assert.AreEqual(key, network.Key, String.Format("Key mismatch on creating SocialWordNetwork. Expected {0}, got {1}.", key, network.Key));
            return network;
        }

        private void TestWordCount(SocialWordNetwork network)
        {
            // check word count
            int expectedWords = DictionariesTests.GetExpectedLength(network.Key);
            int actualWords = network.Words?.Length ?? 0;
            Assert.AreEqual(expectedWords, actualWords, String.Format("Word count mismatch for dictionary '{0}'. Expected {1}, got {2}.", network.Key, expectedWords, actualWords));
        }

        private void TestFriendshipCount(SocialWordNetwork network, int expectedCount)
        {
            // check friendships count
            int actualCount = network.Friendships?.ValueCount ?? 0;
            Assert.AreEqual(expectedCount, actualCount, String.Format("Friendship count mismatch for dictionary '{0}'. Expected {1}, got {2}.", network.Key, expectedCount, actualCount));
        }
    }
}
