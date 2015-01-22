# LevenshteinAutomaton
### Introduction to algorithm
A Levenshtein Automaton implemented in C#, for fuzzy searching.

Given a libary of words, and a given original word W, Levenshtein Automaton can find all the words similar to W from the libary. The similarity is specified by Levenshtein distance.

The word library is constructed into a TRIE strcuture for searching.

When given a word to search, Levenshtein Automaton will first created a NFA (nondeterministic finite automata) based on given word and Max levenshtein distance, then transform the NFA to DFA (deterministic finite automata).
After getting DFA, do depth-first-search in this DFA and use TRIE node to validate the jump between states in DFA duing the DFS. When reaching final states in DFA, the word we reach in TRIE at the same time would be one of the fuzzy search results.

For each word in test case or libarary, non-alphabet character is removed and capital letters are transformed to lowercase in pre-processing.

To view how more details about Levenshtein Automaton, please refer to http://blog.notdot.net/2010/07/Damn-Cool-Algorithms-Levenshtein-Automata.

The codes transforming from NFA to DFA refers related part of Regex Engine, its related codes can be viewed in this link
http://www.leniel.net/2009/05/regex-engine-in-csharp-match-strings.html#sthash.YON5CVU0.dpbs

### Introduction to codes
The project contains implementation of Levenshtein Automaton, and compares its performance when traditional way of fuzzy search (check all words in libraray and calculate Levenshtein distance from given word by Dynamica programming).

"Data" folder/
	WordLib.txt: serves as the libary.
	Three files as test cases, containing words to search.
	
Program.cs
	Main entrance to run the project.

### Running result
Following is result of running test case 2 (TestCase_2_WordsToSearch.txt), with given Max Levenshtein distance value = 1

	----Automaton way----
	results size for "gay": 1
	results size for "add": 1
	results size for "he": 2
	results size for "it": 2
	results size for "share": 1
	results size for "do": 2
	results size for "ye": 1
	results size for "so": 2
	results size for "to": 3
	results size for "she": 1
	results size for "get": 1
	results size for "she": 1
	Total hits: 0; Max distance : 1; Total time consumed(milisec): 166


	----Traditional way----
	results size for "gay": 1
	results size for "add": 1
	results size for "he": 2
	results size for "it": 2
	results size for "share": 1
	results size for "do": 2
	results size for "ye": 1
	results size for "so": 2
	results size for "to": 3
	results size for "she": 1
	results size for "get": 1
	results size for "she": 1
	Total hits: 0; Max distance : 1; Total time consumed (milisec): 4894

