using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordbook.tests
{
    [TestClass]
    public class DictionarySetTests
    {
        const int FOO = 1;
        const int BAR = 2;

        [TestMethod]
        public void TestDictionarySetTryGetValues()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            HashSet<int> hashSet;
            // HashSet should not exist in empty DictionarySet
            Assert.AreEqual(false, dictionarySet.TryGetValues(FOO, out hashSet));
        }

        [TestMethod]
        public void TestDictionarySetAdd()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            // on first add, value should not exist
            Assert.AreEqual(true, dictionarySet.Add(FOO, BAR));
            // on second add, value should exist
            Assert.AreEqual(false, dictionarySet.Add(FOO, BAR));
        }

        [TestMethod]
        public void TestDictionarySetContainKeys()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            // check that keys do not exist in empty DictionarySet
            Assert.AreEqual(false, dictionarySet.ConatinsKey(FOO));
            Assert.AreEqual(false, dictionarySet.ConatinsKey(BAR));

            // add key/value pair, ensure it did not exist before
            Assert.AreEqual(true, dictionarySet.Add(FOO, BAR));
            // check that key exists
            Assert.AreEqual(true, dictionarySet.ConatinsKey(FOO));
            
            // check that wrong key does not exist
            Assert.AreEqual(false, dictionarySet.ConatinsKey(BAR));

            // add inverse key/value pair, ensure it did not exist before
            Assert.AreEqual(true, dictionarySet.Add(BAR, FOO));
            // check that inverse key now exists
            Assert.AreEqual(true, dictionarySet.ConatinsKey(BAR));
        }
    }
}
