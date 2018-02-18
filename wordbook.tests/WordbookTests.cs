using wordbook;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace wordbook.tests
{
    [TestClass]
    public class WordbookTests
    {
        [TestMethod]
        public void TestDictionariesGetSmall()
        {
            Dictionaries.GetDictionary(Dictionaries.Keys.very_small_test_dictionary);
        }
    }
}
