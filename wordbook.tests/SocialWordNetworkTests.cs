using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace wordbook.tests
{
    [TestClass]
    public class SocialWordNetworkTests
    {
        [TestMethod]
        public void TestConstructors()
        {
            TestConstructor(Dictionaries.Keys.dictionary);
            TestConstructor(Dictionaries.Keys.eight_dictionary);
            TestConstructor(Dictionaries.Keys.half_dictionary);
            TestConstructor(Dictionaries.Keys.quarter_dictionary);
            TestConstructor(Dictionaries.Keys.very_small_test_dictionary);
        }

        private void TestConstructor(Dictionaries.Keys key)
        {
            SocialWordNetwork network = new SocialWordNetwork(key);
            // check keys match
            Assert.AreEqual(key, network.Key, String.Format("Key mismatch on creating SocialWordNetwork. Expected {0}, got {1}.", key, network.Key));
            // check word count
            int expectedWords = DictionariesTests.GetExpectedLength(key);
            int actualWords = network.Words.Length;
            Assert.AreEqual(expectedWords, actualWords, String.Format("Word count mismatch for dictionary '{0}'. Expected {1}, got {2}.", key, expectedWords, actualWords));
        }
    }
}
