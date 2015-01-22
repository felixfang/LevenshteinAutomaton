//
//	Regular Expression Engine C# Sample Application
//	2006, Leniel Braz de Oliveira Macaferi & Wellington Magalhães Leite.
//
//  UBM's Computer Engineering - 7th term [http://www.ubm.br/]
//  
//  This program sample was developed and turned in as a term paper for Lab. of
//  Compilers Construction. It was based on the source code provided by Eli Bendersky
//  [http://eli.thegreenplace.net/] and is provided "as is" without warranty.
//

using System;
using SCG = System.Collections.Generic;
using System.Text;

namespace LevenshteinAutomaton
{
  public class Set<T> : C5.HashSet<T>
  {
    public Set(SCG.IEnumerable<T> enm)
      : base()
    {
      AddAll(enm);
    }

    public Set(params T[] elems)
      : this((SCG.IEnumerable<T>)elems)
    {
    }

    // Set union (+), difference (-), and intersection (*):

    public static Set<T> operator +(Set<T> s1, Set<T> s2)
    {
      if(s1 == null || s2 == null)
        throw new ArgumentNullException("Set+Set");
      else
      {
        Set<T> res = new Set<T>(s1);
        res.AddAll(s2);
        return res;
      }
    }

    public static Set<T> operator -(Set<T> s1, Set<T> s2)
    {
      if(s1 == null || s2 == null)
        throw new ArgumentNullException("Set-Set");
      else
      {
        Set<T> res = new Set<T>(s1);
        res.RemoveAll(s2);
        return res;
      }
    }

    public static Set<T> operator *(Set<T> s1, Set<T> s2)
    {
      if(s1 == null || s2 == null)
        throw new ArgumentNullException("Set*Set");
      else
      {
        Set<T> res = new Set<T>(s1);
        res.RetainAll(s2);
        return res;
      }
    }

    // Equality of sets; take care to avoid infinite loops

    public static bool operator ==(Set<T> s1, Set<T> s2)
    {
      return C5.EqualityComparer<Set<T>>.Default.Equals(s1, s2);
    }

    public static bool operator !=(Set<T> s1, Set<T> s2)
    {
      return !(s1 == s2);
    }

    public override bool Equals(Object that)
    {
      return this == (that as Set<T>);
    }

    public override int GetHashCode()
    {
      return C5.EqualityComparer<Set<T>>.Default.GetHashCode(this);
    }

    // Subset (<=) and superset (>=) relation:

    public static bool operator <=(Set<T> s1, Set<T> s2)
    {
      if(s1 == null || s2 == null)
        throw new ArgumentNullException("Set<=Set");
      else
        return s1.ContainsAll(s2);
    }

    public static bool operator >=(Set<T> s1, Set<T> s2)
    {
      if(s1 == null || s2 == null)
        throw new ArgumentNullException("Set>=Set");
      else
        return s2.ContainsAll(s1);
    }

    public override String ToString()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append("{");
      bool first = true;
      foreach(T x in this)
      {
        if(!first)
          sb.Append(",");
        sb.Append(x);
        first = false;
      }
      sb.Append("}");
      return sb.ToString();
    }
  }
}
