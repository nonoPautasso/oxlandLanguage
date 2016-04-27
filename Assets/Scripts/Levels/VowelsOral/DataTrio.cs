using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Levels.VowelsOral
{
    /*
     A data structure to send a trio of values
     */
    public class DataTrio<X, Y, Z>
    {
        private X fst;
        private Y snd;
        private Z thd;

        public DataTrio(X fst, Y snd, Z thd)
        {
            this.fst = fst;
            this.snd = snd;
            this.thd = thd;
        }

		public void SetFst(X fst)
		{
			this.fst = fst;
		}

		public void SetSnd(Y snd)
		{
			this.snd = snd;
		}

		public void SetThd(Z thd)
		{
			this.thd = thd;
		}

        public X Fst()
        {
            return this.fst;
        }

        public Y Snd()
        {
            return this.snd;
        }

        public Z Thd()
        {
            return this.thd;
        }
    }
}
