using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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
            Assert.IsFalse(dictionarySet.TryGetValues(FOO, out hashSet), "Got HashSet for FOO in empty DictionarySet");
            // on first add, value should not exist
            Assert.IsTrue(dictionarySet.Add(FOO, BAR), "FOO/BAR existed upon adding to empty DictionarySet");
            // HashSet should exist now
            Assert.IsTrue(dictionarySet.TryGetValues(FOO, out hashSet), "Didn't get HashSet for FOO in DictionarySet after adding FOO/BAR");
        }

        [TestMethod]
        public void TestDictionarySetAdd()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            // on first add, value should not exist
            Assert.IsTrue(dictionarySet.Add(FOO, BAR), "FOO/BAR existed upon adding to empty DictionarySet");
            // on second add, value should exist
            Assert.IsFalse(dictionarySet.Add(FOO, BAR), "FOO/BAR did not exist upon re-adding to DictionarySet" );
        }

        [TestMethod]
        public void TestDictionarySetContainKeys()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            // check that keys do not exist in empty DictionarySet
            Assert.IsFalse(dictionarySet.ConatinsKey(FOO), "Key FOO existed in empty DictionarySet");
            Assert.IsFalse(dictionarySet.ConatinsKey(BAR), "Key BAR existed in empty DictionarySet");

            // add key/value pair, ensure it did not exist before
            Assert.IsTrue(dictionarySet.Add(FOO, BAR), "FOO/BAR existed when adding to empty DictionarySet");
            // check that key exists
            Assert.IsTrue(dictionarySet.ConatinsKey(FOO), "Key FOO did not exist after adding FOO/BAR to DictionarySet");
            
            // check that wrong key does not exist
            Assert.IsFalse(dictionarySet.ConatinsKey(BAR), "Key BAR exists in DictionarySet before added");

            // add inverse key/value pair, ensure it did not exist before
            Assert.IsTrue(dictionarySet.Add(BAR, FOO), "BAR/FOO existed when adding to DictionarySet that did not contain them");
            // check that inverse key now exists
            Assert.IsTrue(dictionarySet.ConatinsKey(BAR), "Key BAR did not exist after adding BAR/FOO to DictionarySet");
        }

        [TestMethod]
        public void TestDictionarySetContainsValue()
        {
            DictionarySet<int, int> dictionarySet = new DictionarySet<int, int>();
            // add key/value pair, ensure it did not exist before
            Assert.IsTrue(dictionarySet.Add(FOO, BAR), "FOO/BAR existed when adding to empty DictionarySet");
            // ensure key/value pair exists
            Assert.IsTrue(dictionarySet.ContainsValue(FOO, BAR), "FOO/BAR did not exist after being added to DictionarySet (function)");
            Assert.IsTrue(dictionarySet[FOO, BAR], "FOO/BAR did not exist after being added to DictionarySet (getter)");
            // ensure inverse pair doesn't exist
            Assert.IsFalse(dictionarySet.ContainsValue(BAR, FOO), "BAR/FOO existed before being added to DictionarySet (function)");
            Assert.IsFalse(dictionarySet[BAR, FOO], "BAR/FOO existed before being added to DictionarySet (getter)");
            // add inverse key/value pair, ensure it did not exist before
            Assert.IsTrue(dictionarySet.Add(BAR, FOO), "BAR/FOO existed upon first add to DictionaryList");
            // ensure inverse pair now exists
            Assert.IsTrue(dictionarySet.ContainsValue(BAR, FOO), "BAR/FOO did not exist after being added to DictionaryList (function)");
            Assert.IsTrue(dictionarySet[BAR, FOO], "BAR/FOO did not exist after being added to DictionaryList (getter)");
        }

    }
}
