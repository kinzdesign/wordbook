using wordbook;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wordbook.tests
{
    [TestClass]
    public class DictionariesTests
    {
        #region Dictionaries

        [TestMethod]
        public void TestDictionariesGetSmall()
        {
            TestDictionary(Dictionaries.Keys.very_small_test_dictionary, 12);
        }

        [TestMethod]
        public void TestDictionariesGetEight()
        {
            TestDictionary(Dictionaries.Keys.eight_dictionary, 22339);
        }

        [TestMethod]
        public void TestDictionariesGetQuarter()
        {
            TestDictionary(Dictionaries.Keys.quarter_dictionary, 44674);
        }

        [TestMethod]
        public void TestDictionariesGetHalf()
        {
            TestDictionary(Dictionaries.Keys.half_dictionary, 89346);
        }

        [TestMethod]
        public void TestDictionariesGetFull()
        {
            TestDictionary(Dictionaries.Keys.dictionary, 93461); // TODO: 178692);
        }

        private void TestDictionary(Dictionaries.Keys key, int expectedLength)
        {
            var dict = Dictionaries.Get(key);
            Assert.IsNotNull(dict);
            Assert.IsInstanceOfType(dict, typeof(string[]));
            Assert.AreEqual(expectedLength, dict.Length);
        }

        #endregion
    }
}
