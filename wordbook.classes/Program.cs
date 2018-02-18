using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionaries.GetDictionary(Dictionaries.Keys.very_small_test_dictionary);
            Console.ReadKey();
        }
    }
}
