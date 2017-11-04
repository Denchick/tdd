﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Architecture.TagTypes
{
    class SmallTag : Tag
    {
        public override Brush Brush => new SolidBrush(Color.FromArgb(156, 89, 44));
        public override Font Font => new Font("Arial", 12.0f);
    }
}
