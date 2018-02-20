# Kinsey Roberts's Code Sample for ApartmentList

## To run

The code compiles for both .NET Framework 4.0 and .NET Core 2.0. 
The latter should allow for cross-platform execution on Linux or macOS systems with .NET Core installed.

* If running Windows with .NET Framework 4.0 or greater installed: 
    1. `cd wordbook.net.4.0\bin\Release`
    2. `wordbook.exe`
* If running Linux, or macOS with .NET Core 2.0 installed:
    1. `cd wordbook.net.core.2.0`
    2. `dotnet run`

### Arguments

Command line arguments can be used to customize the dictionary used, the word searched for, and whether to display the words in the extended network:

    WORDBOOK [/?] [/V] [/D=X] [word]
      [word]   Specifies word to search for, default is LISTY
      /V       Verbose mode, lists words in extended network
      /E       Show elapsed times for subtasks
      /D=X     Specifies dictionary to search, default is D
                 D or F - dictionary.txt
                 H or 2 - half_dictionary.txt
                 Q or 4 - quarter_dictionary.txt
                 E or 8 - eight_dictionary.txt
                 S or T - very_small_test_dictionary.txt
      /?       Displays this help

Note, '/?' switch does not work with the `dotnet run` command, it gets intercepted by .NET Core.

## Approach

My approach is two-step. First, scan the dictionary to build the graph of direct friendships. Then, to find the size of the social network for a given word, traverse the graph and count the extended friend network.

My initial approach involved pairwise comparison of *all* strings of appropriate length in order to build the direct friendship data. I worked through my initial approach on paper in pseudocode to make sure I had a good idea of how the algorithms worked.

I suspected from the beginning that using prefix tries would yield a (non-asymptotic) performance gain. I worked out a trie-based approach on paper using the very small test dictionary. My hunch was correct: switching to prefix tries allowed me to eliminate multiple strings at a time by ignoring subtrees with edit distance greater than 1. Working the problem out on paper using the very small test dictionary also gave me the data I would need to test my code. 

### First Pass

Three data structures are populated during the first pass:

*   `string[] Words`
    *   Contains all the words from the selected dictionary
    *   The zero-indexed line number in the file/array is used as the integer ID throughout the algorithms
        *   Prevents having to store multiple copies of the strings in memory
    *   Since dictionaries are alphabetical, this array will be used for binary search
        *   Only requires one copy of dictionary to be in memory, rather than creating a `Dictionary<string, int>` hashtable to lookup ID from word
            *   The only lookup is to find the line number of the query word (e.g. "LISTY"), so even if the input weren't alphabetized, it would likely be worth the linear search through the file to avoid the memory overhead of the lookup hashtable
*   `Dictionary<int, SingleEditDistanceTrie> TriesByLength`
    *   `SingleEditDistanceTrie` is a modified prefix trie
        *   `Length` must be set at creation and all strings must match the length
        *   Provides specialized traversals to find a given words' peers (with Levenshtein distance = 1)
*   `DictionarySet<int, int> Friendships`
    *   `DictionarySet<K, V>` is a wrapper around a `Dictionary<K, HashSet<V>>`
        *   Creates the inner HashSets as needed
        *   Handles null (and sanity) checks for an easier-to-read syntax

1.  Read dictionary file into `words`
2.  Iterate lines
    1. Identify direct friends by computing edit distances
        1.  Search for single substitutions in `TriesByLength[line.Length]`
        2.  Search for single insertions in `TriesByLength[line.Length + 1]`
        3.  Search for single deletions in `TriesByLength[line.Length - 1]`
    2.  When a direct friendship is found (two strings such that edit distance = 1), add to adjacency list `Friendships` (two entries, since friendships are reciprocal)
    3.  Add word to the appropriate trie in `TriesByLength`

### Second Pass

The second pass completes a breadth-first search in `Friendships` beginning from the query word

1.  Lookup the line number of the word being searched for
2.  Add this line number to a queue
3.  While the queue has items:
    1.  Dequeue an item
    2.  Add it to the extended network
    3.  For each of its single-edit peers:
        1.  If peer is not already in the extended network:
            1.  Enqueue the peer to be processed
