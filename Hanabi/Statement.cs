using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hanabi
{
    class Statement
    {
        private string mName;
        private string mIndex;
        private int[] mIndexes;
        CardColor mColor;
        CardRank mValue;

        public Statement(string name,string index)
        {
            mName = name;
            mIndex = index;
        }
    }
}
