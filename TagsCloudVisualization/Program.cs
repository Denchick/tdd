using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class Program
    {
        public static void Main()
        {
            Application.Run(new CloudForm { ClientSize = new Size(800, 600) });
        }
    }
}
