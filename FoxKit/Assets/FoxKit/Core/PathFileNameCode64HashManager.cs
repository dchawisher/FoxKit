﻿namespace FoxKit.Core
{
    using FoxKit.Utils;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using UnityEngine;
    using UnityEngine.Assertions;

    public class PathFileNameCode64HashManager : IHashManager<ulong>
    {
        private readonly Dictionary<ulong, string> lookUpTable = new Dictionary<ulong, string>();

        public void LoadDictionary(TextAsset dictionary)
        {
            var linesInFile = dictionary.text.Split('\n');
            foreach (var line in linesInFile)
            {
                var lineWithoutNewLines = Regex.Replace(line, @"\t|\n|\r", string.Empty);
                var hash = HashString(lineWithoutNewLines);
                if (!this.lookUpTable.ContainsKey(hash))
                {
                    this.lookUpTable.Add(hash, lineWithoutNewLines);
                }
            }
        }

        public bool TryGetStringFromHash(ulong hash, out string result)
        {
            return this.lookUpTable.TryGetValue(hash, out result);
        }

        public ulong GetHash(string input)
        {
            return HashString(input);
        }

        public static ulong HashString(string input)
        {
            Assert.IsNotNull(input, "Hash input must not be null.");

            var hash = Hashing.HashFileNameWithExtension(input);
            return hash;
        }

        public string UnhashString(ulong hash)
        {
            string output;
            this.lookUpTable.TryGetValue(hash, out output);
            return output;
        }

        ///// <summary>
        ///// Attempts to unhash a hash. If said attempt succeeds, the returned StringPair is set to string mode, if not, the returned StringPair is set to hash mode.
        ///// </summary>
        ///// <param name="hash">The hash to attempt to unhash.</param>
        ///// <returns>The StringPair derived from the unhash attempt.</returns>
        //public PathFileNameCode64StringPair GetStringPairFromUnhashAttempt(ulong hash)
        //{
        //    return this.TryUnhash(hash, (hashValue => new PathFileNameCode64StringPair(hashValue.ToString(), IsStringOrHash.Hash)), (unhashedString => new PathFileNameCode64StringPair(unhashedString, IsStringOrHash.String)));
        //}

        ///// <summary>
        ///// Gets a hash from a string pair.
        ///// </summary>
        ///// <param name="stringPair">String pair.</param>
        ///// <returns>The hash from string pair.</returns>
        //public ulong GetHashFromStringPair(PathFileNameCode64StringPair stringPair)
        //{
        //    return this.RetrieveHashFromStringPair(stringPair);
        //}
    }
}