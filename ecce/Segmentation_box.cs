using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class Segmentation_box
    {
        public Rectangle rect { get; set; }

        public int column { get; set; }
        public int box_row { get; set; }
        public int index_contour { get; set; }

    }
}
