using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LevenshteinAutomaton
{
    /// <summary>
    /// Each node in the Trie.
    /// </summary>
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public string Key;
        public bool End;

        public TrieNode(string key)
        {
            Children = new Dictionary<char, TrieNode>();
            Key = key;
            End = false;
        }
    }

    /// <summary>
    /// Dictionary which maintains the root node of Trie;
    /// </summary>
    public class TrieDictionary
    {
        private TrieNode _root;

        public TrieNode Root
        {
            get { return _root; }
        }

        public TrieDictionary()
        {
            _root = new TrieNode("");
        }

        public void AddTrieNode(string word)
        {
            if (string.IsNullOrEmpty(word)) return;
            //Regex rgx = new Regex("[^a-zA-Z]");
            //word = rgx.Replace(word, "");
            //word = word.ToLowerInvariant();
            TrieNode cur = _root;
            for (int i = 0; i < word.Length; ++i)
            {
                if (!cur.Children.ContainsKey(word[i]))
                {
                    cur.Children.Add(word[i], new TrieNode(word.Substring(0, i + 1)));
                }
                cur = cur.Children[word[i]];
                if (i == word.Length - 1)
                    cur.End = true;
            }
        }
        
        public static TrieDictionary BuildTrieDictionary(IEnumerator<string> it)
        {
            TrieDictionary trieDict = new TrieDictionary();
            while (it.MoveNext())
            {
                trieDict.AddTrieNode(it.Current);
            }

            return trieDict;
        }
    }
}
