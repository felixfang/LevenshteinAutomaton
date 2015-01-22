using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;

namespace LevenshteinAutomaton
{
    /// <summary>
    /// Implements a levenshtein automaton to search all the words in dictionary whose distance is smaller or equal than given value from given word.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point of the Console Application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Load word libary
            Regex rgx = new Regex("[^a-zA-Z]");
            IEnumerable<string> wordLib = System.IO.File.ReadAllLines(@"Data\WordLib.txt").ToList().Select(word => rgx.Replace(word, "").ToLowerInvariant());

            //The max distance from word, we'll generate all the "brothers" of given word - those words whose distance are within MaxDist from given word.
            //So the bigger MaxDist is, the more "brothers" we will generate based on given words, the longer it takes in overall fuzzy searching.
            const int MaxDist = 1;
            
            //Load test cases
            IEnumerable<string> testcase1 = System.IO.File.ReadAllLines(@"Data\TestCase_2_WordsToSearch.txt").ToList().Select(word => rgx.Replace(word, "").ToLowerInvariant());

            Console.WriteLine("----Automaton way----");
            //Build Trie dictionary based on library.
            TrieDictionary dict = TrieDictionary.BuildTrieDictionary(wordLib.GetEnumerator());

            Stopwatch st = new Stopwatch();
            int hits = 0;
            st.Start();
            foreach (string word in testcase1)
            {
                IEnumerable<string> results = AutomatonSearch.search(word, MaxDist, dict).Distinct();
                if (results.Count() > 0)
                {
                    Console.WriteLine("results size for \"" + word + "\": " + results.Count().ToString());
                }
            }
            st.Stop();
            Console.WriteLine("Total hits: " + hits.ToString() + "; Max distance : " + MaxDist.ToString() + "; Total time consumed(milisec): " + st.ElapsedMilliseconds.ToString());

            Console.Write("\n\n");

            Console.WriteLine("----Traditional way----");
            st.Reset();
            hits = 0;
            //const double factor = 0.7;
            st.Start();
            foreach (string word in testcase1)
            {
                IEnumerable<string> results2 = TraditionSearch.search(word, MaxDist, wordLib.GetEnumerator()).Distinct();
                if (results2.Count() > 0)
                {
                    Console.WriteLine("results size for \"" + word + "\": " + results2.Count().ToString());
                }
            }
            st.Stop();
            Console.WriteLine("Total hits: " + hits.ToString() + "; Max distance : " + MaxDist.ToString() + "; Total time consumed (milisec): " + st.ElapsedMilliseconds.ToString());

            Console.ReadKey();
        }

    }
}