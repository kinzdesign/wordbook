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
    }
}
