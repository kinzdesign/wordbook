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
        public static string[] Get(Keys key)
        {
            // generate resouce name
            string resourceName = String.Format("wordbook.dictionaries.{0}.txt", key);
            // reflect into assembly to get stream to dictionary
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                // temporary list to hold lines
                List<string> lines = new List<string>();
                // read each line into lines
                while (!reader.EndOfStream)
                    lines.Add(reader.ReadLine());
                // return list as array
                return lines.ToArray();
            }
        }
    }
}
