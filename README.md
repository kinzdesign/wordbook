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

Four data structures are populated during the first pass:

*	`string[] words`
*	`Dictionary<string, int> lineLookup`
*	`DictionarySet<int, int> byLength`
*	`DictionarySet<int, int> friends`

1.	Read dictionary file as `string[]`
2.	Iterate lines
	1. 