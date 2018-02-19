using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
