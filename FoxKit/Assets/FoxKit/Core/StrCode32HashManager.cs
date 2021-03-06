﻿namespace FoxKit.Core
{
    using FoxKit.Utils;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using UnityEngine;
    using UnityEngine.Assertions;

    public class StrCode32HashManager : IHashManager<uint>
    {
        private readonly Dictionary<uint, string> lookUpTable = new Dictionary<uint, string>();

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

        public bool TryGetStringFromHash(uint hash, out string result)
        {
            return this.lookUpTable.TryGetValue(hash, out result);
        }

        public uint GetHash(string input)
        {
            return HashString(input);
        }

        public static uint HashString(string input)
        {
            Assert.IsNotNull(input, "Hash input must not be null.");

            var hash = (uint)Hashing.HashFileNameLegacy(input);
            return hash;
        }

        public string UnhashString(uint hash)
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
        //public StrCode32StringPair GetStringPairFromUnhashAttempt(uint hash)
        //{
        //    return this.TryUnhash(hash, hashValue => new StrCode32StringPair(hashValue), unhashedString => new StrCode32StringPair(unhashedString));
        //}

        ///// <summary>
        ///// Gets a hash from a string pair.
        ///// </summary>
        ///// <param name="stringPair">String pair.</param>
        ///// <returns>The hash from string pair.</returns>s
        //public uint GetHashFromStringPair(StrCode32StringPair stringPair)
        //{
        //    return this.RetrieveHashFromStringPair(stringPair);
        //}
    }
}