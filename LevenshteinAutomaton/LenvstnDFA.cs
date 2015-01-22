using System;
using System.Collections.Generic;

using state = System.Int32;
using input = System.Char;

namespace LevenshteinAutomaton
{
  /// <summary>
  /// Implements a deterministic finite automata
  /// </summary>
    public class LenvstnDFA
    {
        // Start state
        public state start;
        // Set of final states
        public Set<state> final;
        // Transition table
        public SortedList<KeyValuePair<state, input>, state> transTable;
        public Dictionary<state, state> defaultTrans;

        public LenvstnDFA()
        {
            final = new Set<state>();

            defaultTrans = new Dictionary<state, state>();
            transTable = new SortedList<KeyValuePair<state, input>, state>(new Comparer());
        }


        public void Show()
        {
            Console.Write("DFA start state: {0}\n", start);
            Console.Write("DFA final state(s): ");

            IEnumerator<state> iE = final.GetEnumerator();

            while (iE.MoveNext())
                Console.Write(iE.Current + " ");

            Console.Write("\n\n");

            foreach (KeyValuePair<KeyValuePair<state, input>, state> kvp in transTable)
                Console.Write("Trans[{0}, {1}] = {2}\n", kvp.Key.Key, kvp.Key.Value, kvp.Value);

            foreach (KeyValuePair<state, state> kvp in defaultTrans)
                Console.Write("Default trans[{0}] = {1}\n", kvp.Key, kvp.Value);
        }
    }

    /// <summary>
    /// Implements a comparer that suits the transTable SordedList
    /// </summary>
    public class Comparer : IComparer<KeyValuePair<state, input>>
    {
        public int Compare(KeyValuePair<state, input> transition1, KeyValuePair<state, input> transition2)
        {
            if (transition1.Key == transition2.Key)
                return transition1.Value.CompareTo(transition2.Value);
            else
                return transition1.Key.CompareTo(transition2.Key);
        }
    }

}