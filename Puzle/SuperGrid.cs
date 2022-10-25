using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Puzle
{
    internal class SuperGrid : Grid
    {
        public SuperButton Forat { get; set; }
        public int NFiles { get; set; }
        public int NColumnes { get; set; }
        public int[] PosicionsFitxes { get; set; }
        public int NCorrectes { get; set; }
    }
}
