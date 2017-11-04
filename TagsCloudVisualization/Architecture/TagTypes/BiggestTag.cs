using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Architecture.TagTypes
{
    class BiggestTag : Tag
    {
        public override Brush Brush => Brushes.White;
        public override Font Font => new Font("Arial", 64.0f);
    }
}
