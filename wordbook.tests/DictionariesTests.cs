using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace wordbook.tests
{
    [TestClass]
    public class DictionariesTests
    {
        [TestMethod]
        public void TestDictionariesGetSmall()
        {
            TestDictionary(Dictionaries.Keys.very_small_test_dictionary);
        }

        [TestMethod]
        public void TestDictionariesGetEight()
        {
            TestDictionary(Dictionaries.Keys.eight_dictionary);
        }

        [TestMethod]
        public void TestDictionariesGetQuarter()
        {
            TestDictionary(Dictionaries.Keys.quarter_dictionary);
        }

        [TestMethod]
        public void TestDictionariesGetHalf()
        {
            TestDictionary(Dictionaries.Keys.half_dictionary);
        }

        [TestMethod]
        public void TestDictionariesGetFull()
        {
            TestDictionary(Dictionaries.Keys.dictionary);
        }

        private void TestDictionary(Dictionaries.Keys key)
        {
            var dict = Dictionaries.Get(key);
            Assert.IsNotNull(dict, String.Format("Dictionary {0} is null", key));
            Assert.IsInstanceOfType(dict, typeof(string[]), String.Format("Dictionary {0} is not a string array", key));
            int expectedLength = GetExpectedLength(key);
            Assert.AreEqual(expectedLength, dict.Length, String.Format("Dictionary {0} has wrong length", key));
        }

        internal static int GetExpectedLength(Dictionaries.Keys key)
        {
            switch (key)
            {
                case Dictionaries.Keys.dictionary:
                    return 178692;
                case Dictionaries.Keys.eight_dictionary:
                    return 22339;
                case Dictionaries.Keys.half_dictionary:
                    return 89346;
                case Dictionaries.Keys.quarter_dictionary:
                    return 44674;
                case Dictionaries.Keys.very_small_test_dictionary:
                    return 12;
            }
            return -1;
        }
    }
}
