using System;

namespace wordbook
{
    public class Program
    {
        public static void Main(string commandName, string[] args)
        {
            // set defaults
            Dictionaries.Keys dict = Dictionaries.Keys.dictionary;
            string word = "LISTY";
            bool showWords = false;
            bool showElapsed = false;

            // parse command line arguments if present
            if (args != null && args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    string arg = args[i].ToUpper();
                    // check first two chars of arg for switch character
                    switch (arg.Substring(0, 2))
                    {
                        case @"/H":
                        case @"/?":
                            // display help
                            PrintHelp(commandName);
                            return;
                        case @"/D":
                            // get dictionary
                            dict = GetDictionaryKey(arg, dict);
                            break;
                        case @"/V":
                            // verbose mode
                            showWords = true;
                            break;
                        case @"/E":
                            // show elapsed times
                            showElapsed = true;
                            break;
                        default:
                            // not a switch
                            word = arg;
                            break;
                    }
                }
            }

            // write output
            Console.WriteLine("Extended Network of {0} in {1}.txt", word, dict);

            // create network
            var network = new SocialWordNetwork(dict);
            Console.WriteLine("  Dictionary Size:   {0}", network.Words.Length);
            // get extended network
            var extended = network.GetExtendedNetwork(word);

            // write error if not found
            if (extended.Length == 0)
                Console.WriteLine("  ERROR:             {0} not found in {1}", word, dict);
            // write size
            Console.WriteLine("  Network Size:      {0}", extended.Length);
            // output words if requested
            if (showWords && extended.Length > 0)
            { 
                Console.WriteLine("  Network Members:");
                Console.WriteLine("    {");
                for (int i = 0; i < extended.Length; i++)
                    Console.WriteLine("      {0}{1}", network.Words[extended[i]], i + 1 == extended.Length ? String.Empty : ",");
                Console.WriteLine("    }");
                // output size again if list is long
                if(extended.Length > 20)
                    Console.WriteLine("  Network Size:      {0}", extended.Length);
            }
            // output elapsed times if requested
            if(showElapsed)
            {
                Console.WriteLine("  Elapsed Times:");
                WriteElapsedTime("LoadDictionary:      ", network.ElapsedLoadDictionary);
                WriteElapsedTime("BuildNetwork:        ", network.ElapsedBuildNetwork);
                WriteElapsedTime("GetExtendedNetwork:  ", network.ElapsedGetExtendedNetwork);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void WriteElapsedTime(string functionHeader, TimeSpan? elapsed)
        {
            if(elapsed.HasValue)
                Console.WriteLine("    {0}{1}", functionHeader, elapsed.Value);
        }

        private static Dictionaries.Keys GetDictionaryKey(string arg, Dictionaries.Keys nullValue)
        {
            // should be of form /D=X
            if (arg.Length == 4)
            {
                // look at last character
                switch (arg[3])
                {
                    case 'D':
                    case 'F':
                        return Dictionaries.Keys.dictionary;
                    case 'E':
                    case '8':
                        return Dictionaries.Keys.eight_dictionary;
                    case 'H':
                    case '2':
                        return Dictionaries.Keys.half_dictionary;
                    case 'Q':
                    case '4':
                        return Dictionaries.Keys.quarter_dictionary;
                    case 'S':
                    case 'T':
                        return Dictionaries.Keys.very_small_test_dictionary;
                    default:
                        break;
                }
            }
            return nullValue;
        }

        private static void PrintHelp(string commandName)
        {
            Console.WriteLine();
            Console.WriteLine("Wordbook - The Social Network For Words, by: Kinsey Roberts");
            Console.WriteLine("Computes the extended social network of a word in a dictionary");
            Console.WriteLine();
            Console.WriteLine("{0} [/?] [/V] [/D=X] [word]", commandName);
            Console.WriteLine();
            Console.WriteLine("  [word]   Specifies word to search for, default is LISTY");
            Console.WriteLine("  /V       Verbose mode, lists words in extended network");
            Console.WriteLine("  /E       Show elapsed times for subtasks");
            Console.WriteLine("  /D=X     Specifies dictionary to search, default is D");
            Console.WriteLine("             D or F - dictionary.txt");
            Console.WriteLine("             H or 2 - half_dictionary.txt");
            Console.WriteLine("             Q or 4 - quarter_dictionary.txt");
            Console.WriteLine("             E or 8 - eight_dictionary.txt");
            Console.WriteLine("             S or T - very_small_test_dictionary.txt");
            Console.WriteLine("  /?       Displays this help");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
