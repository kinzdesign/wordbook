using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace wordbook
{
    public static class Dictionaries
    {
        public enum Keys
        {
            dictionary,
            eight_dictionary,
            half_dictionary,
            quarter_dictionary,
            very_small_test_dictionary
        }

        /// <summary>
        /// Retreives a dictionary from the embedded resources as a string array of lines
        /// </summary>
        /// <param name="key">The dictionary key (filename without extension)</param>
        /// <returns>An array containing the lines of the dictionary file</returns>
        public static string[] GetDictionary(Keys key)
        {
            throw new NotImplementedException();
        }
    }
}
