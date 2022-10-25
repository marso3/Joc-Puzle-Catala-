using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Puzle
{
    public static class Extensions
    {
        public static void Swap<T>(this List<T> list, SuperButton i, SuperButton j)
        {
            T temp = list[i.NActual - 1];
            list[i.NActual - 1] = list[j.NActual - 1];
            list[j.NActual - 1] = temp;
        }
    }
}
