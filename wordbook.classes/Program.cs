﻿using System;

namespace wordbook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            foreach (string word in Dictionaries.Get(Dictionaries.Keys.very_small_test_dictionary))
                Console.WriteLine(word);
            Console.WriteLine("--EOF--");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
