using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wordbook
{
    public class DictionarySet<K, V>
    {
        #region private variables

        private Dictionary<K, HashSet<V>> dictionary = new Dictionary<K, HashSet<V>>();

        #endregion

        #region public functions

        /// <summary>
        /// Tries to find the HashSet associated with <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <param name="values">HashSet<<typeparamref name="V"/>> to be populated if found</param>
        /// <returns>True if <paramref name="key"/> was found and <paramref name="values"/> is populated; false otherwise</returns>
        public bool TryGetValues(K key, out HashSet<V> values)
        {
            return dictionary.TryGetValue(key, out values);
        }

        #endregion
    }
}
