using System;
using System.Collections.Generic;
using System.Text;

namespace FillLatinSquare
{
    public static class Util
    {
        public static void CopyTo(this List<int> s, List<int> otherS)
        {
            if (otherS.Count > 0)
            {
                otherS.Clear();
            }
            s.ForEach(e => otherS.Add(e));
        }
    }
}
