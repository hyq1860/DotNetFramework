using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace DotNet.Search
{
    class TSTree
    {

        public static TSTree Instance()
        {
            return Nested.instance;
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly TSTree instance = new TSTree();
        }

        private static string[] _specialchars = new string[] { "\\", "+", "-", "&&", "||", "!", "(", ")", "{", "}", "[", "]", "^", "\"", "~", "*", "?", ":" };

        public class TSTNode
        {
            /** The key to the node. */
            public int data = -1;//save data

            /** The relative nodes. */
            public TSTNode LOKID;//left
            public TSTNode EQKID;//middle
            public TSTNode HIKID;//right

            /** The char used in the split. */
            public char splitchar;//the char of node 

            /**
                *  Constructor method.
                *
                *@param  splitchar  The char used in the split.
                *@param  parent     The parent node.
                */
            public TSTNode(char splitchar)
            {
                this.splitchar = splitchar;
            }
        }
        public static TSTNode rootNode;
        //building TstNode tree
        private TSTree()
        {
            String word;
            for (int i = 0; i < _specialchars.Length; i++)
            {
                word = _specialchars[i];
                String key = word;
                word = key;
                if (rootNode == null)
                {
                    rootNode = new TSTNode(key[0]);
                }
                //  TSTNode node = null;
                if (key.Length > 0 && rootNode != null)
                {
                    TSTNode currentNode = rootNode;
                    currentNode =
                        getOrCreateNode(word);
                    currentNode.data = 1;
                }
            }
        }

        /**
      *  Returns the node indexed by key, creating that node if it doesn't exist,
      *  and creating any required intermediate nodes if they don't exist.
      *
      *@param  key                           A <code>String</code> that indexes the node that is returned.
      *@return                                  The node object indexed by key. This object is an
      *                                               instance of an inner class named <code>TernarySearchTrie.TSTNode</code>.
      *@exception  NullPointerException      If the key is <code>null</code>.
      *@exception  IllegalArgumentException  If the key is an empty <code>String</code>.
      */
        private TSTNode getOrCreateNode(String key)
        {
            if (key == null || key.Length == 0)
            {
                throw new Exception("NullPointerException");
            }
            if (rootNode == null)
            {
                rootNode = new TSTNode(key[0]);
            }
            TSTNode currentNode = rootNode;
            int charIndex = 0;
            while (true)
            {
                int charComp = (
                        key[charIndex] -
                        currentNode.splitchar);
                if (charComp == 0)
                {
                    charIndex++;
                    if (charIndex == key.Length)
                    {
                        return currentNode;
                    }
                    if (currentNode.EQKID == null)
                    {
                        currentNode.EQKID =
                            new TSTNode(key[charIndex]);
                    }
                    currentNode = currentNode.EQKID;
                }
                else if (charComp < 0)
                {
                    if (currentNode.LOKID == null)
                    {
                        currentNode.LOKID =
                            new TSTNode(key[charIndex]);
                    }
                    currentNode = currentNode.LOKID;
                }
                else
                {
                    if (currentNode.HIKID == null)
                    {
                        currentNode.HIKID =
                            new TSTNode(key[charIndex]);
                    }
                    currentNode = currentNode.HIKID;
                }
            }
        }

        public enum Prefix
        {
            Match, MisMatch, MatchPrefix //匹配 ，不匹配 ，前缀匹配
        }


        private TSTNode getNode(String key, TSTNode startNode) //checkPrefix 用到此函数
        {
            if (key == null || key.Length == 0)
            {
                return null;
            }

            TSTNode currentNode = startNode;
            int charIndex = 0;
            while (true)
            {
                if (currentNode == null)
                {

                    //System.Console.WriteLine("currentNode == null");
                    return null;
                }
                int charComp = key[charIndex] - currentNode.splitchar;
                if (charComp == 0)
                {
                    charIndex++;
                    //System.Console.WriteLine("charIndex:"+charIndex+" key length:"+ key.Length );
                    if (charIndex == key.Length)
                    {
                        return currentNode;
                    }

                    currentNode = currentNode.EQKID;
                }
                else if (charComp < 0)
                {
                    currentNode = currentNode.LOKID;
                }
                else
                {
                    currentNode = currentNode.HIKID;
                }
            }
        }


        public TSTNode getNode(String key)
        {
            return getNode(key, rootNode);
        }

        public Prefix checkPrefix(String prefix, TSTNode start)
        {
            TSTNode startNode = getNode(prefix, start);
            //System.out.println("the result of node split char:"+startNode.splitchar);
            if (startNode == null)
            {
                return Prefix.MisMatch;
            }
            if (startNode.data != -1)
            {
                return Prefix.Match;
            }
            return Prefix.MatchPrefix;
        }
    }
}
