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

        /// <summary>
        /// Adds <paramref name="val"/> to the HashSet for <paramref name="key"/>. 
        /// Returns bool indicating whether item was added (won't be added if already present)
        /// </summary>
        /// <param name="key">Key to add to</param>
        /// <param name="val">Value to add</param>
        /// <returns>true if added, false if already present</returns>
        public bool Add(K key, V val)
        {
            HashSet<V> set;
            // if bin for key does not exist, initialize it and add to dictionary
            if(!TryGetValues(key, out set))
            {
                set = new HashSet<V>();
                dictionary.Add(key, set);
            }
            return set.Add(val);
        }

        /// <summary>
        /// Checks whether there is a HashSet for the given <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key to search for</param>
        /// <returns>Whther the HashSet exists</returns>
        public bool ConatinsKey(K key)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
