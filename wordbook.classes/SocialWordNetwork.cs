﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wordbook
{
    public class SocialWordNetwork
    {
        #region instance variables

        public Dictionaries.Keys Key { get; private set; }

        private string[] _words;
        public string[] Words
        {
            get
            {
                // lazy-load dictionary on first use
                if (_words == null)
                    _words = Dictionaries.Get(Key);
                return _words;
            }
        }

        #endregion

        #region constructors

        public SocialWordNetwork(Dictionaries.Keys key)
        {
            // record the key
            Key = key;
        }

        #endregion

    }
}
