# Kinsey Roberts's Code Sample for ApartmentList

## To run

The code compiles for both .NET Framework 4.0 and .NET Core 2.0. 
The latter should allow for cross-platform execution on Linux or macOS systems with .NET Core installed.

* If running Windows with .NET Framework 4.0 or greater installed: 
    1. `cd wordbook.net.4.0\bin\Debug`
    2. `wordbook.exe`
* If running Linux, or macOS with .NET Core 2.0 installed:
    1. `cd wordbook.net.core.2.0`
    2. `dotnet run`

## Approach

My approach is two-step. First, scan the dictionary to build the graph of direct friendships. Then, to find the size of the social network for a given word, traverse the graph and count the extended friend network.

### First Pass

Three data structures are populated during the first pass:

*	`string[] words`
*	`DictionarySet<int, int> byLength`
*	`DictionarySet<int, int> friends`

1.	Read dictionary file into `words`
2.	Iterate lines
	1. Identify direct friends by computing edit distances
		1.	Search for single substitutions in `byLength[line.Length]`
		2.	Search for single insertions in `byLength[line.Length + 1]`
		3.	Search for single deletions in `byLength[line.Length - 1]`
	2.	When a direct friendship is found (two strings such that edit distance = 1), add to adjaency list `friends` (two entries, since friendships are reciprocal)

### Second Pass

TODO: Run BFS from source, count size of extended network. Use care notto double-count nodes

