using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ecce
{
    internal class CollectionSegmentBox
    {
        public Rectangle Rect { get; set; }

        public int Column { get; set; }
        public int RowOfBox { get; set; }
        public int IdxContour { get; set; }

    }
}
