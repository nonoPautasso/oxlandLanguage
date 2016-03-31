using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Levels.Vowels
{
    /*
    A data structure to send a pair of values
    */
    public class DataPair<X, Y>
    {
        private X fst;
        private Y snd;

        public DataPair(X fst, Y snd)
        {
            this.fst = fst;
            this.snd = snd;
        }

        public X Fst()
        {
            return this.fst;
        }

        public Y Snd()
        {
            return this.snd;
        }
    }
}
