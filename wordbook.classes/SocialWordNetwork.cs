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
