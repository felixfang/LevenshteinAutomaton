using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LevenshteinAutomaton
{
    public class AutomatonSearch
    {
        private static void DFSserach(LenvstnDFA dfa, int start, TrieNode dictNode, List<string> output)
        {
            if (dfa.final.Contains(start) && dictNode.End)
                output.Add(dictNode.Key);

            Set<char> inputs = new Set<char>();
            for (char ch = 'a'; ch <= 'z'; ++ch)
            {
                KeyValuePair<int, char> pair = new KeyValuePair<int, char>(start, ch);
                if (dfa.transTable.ContainsKey(pair))
                {
                    inputs.Add(ch);
                    if (dictNode.Children.ContainsKey(ch))
                    {
                        DFSserach(dfa, dfa.transTable[pair], dictNode.Children[ch], output);
                    }
                }
            }

            if (dfa.defaultTrans.ContainsKey(start))
            {
                foreach (char input in dictNode.Children.Keys)
                {
                    if (!inputs.Contains(input))
                    {
                        DFSserach(dfa, dfa.defaultTrans[start], dictNode.Children[input], output);
                    }
                }
            }
        }

        public static IEnumerable<string> search(string oriWord, int maxDist, TrieDictionary dict)
        {
            LenvstnNFA nfa = LenvstnNFA.BuildNFA(oriWord, maxDist);
            //nfa.Show();
            LenvstnDFA dfa = SubsetMachine.SubsetConstruct(nfa);
            //dfa.Show();
            List<string> output = new List<string>();
            DFSserach(dfa, dfa.start, dict.Root, output);
            return output;
        }
    }
}
