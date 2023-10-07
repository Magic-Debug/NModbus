using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;


namespace Wintellect.PowerCollections
{



    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        public static void Main()
        {


            var list = new List<Int64>();
            list.Add(134);

            MultiDictionary<int, int> dic = new MultiDictionary<int, int>(true);

            dic.Add(1,100);


            Algorithms.Count<Int64>(list);




        }
    }
}
