using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    public partial class CloudForm : Form
    {
        private static readonly Dictionary<string, int> tagsDictionary = new Dictionary<string, int>()
        {
            {"Participation", 99},
            {"Usability", 98},
            {"Design", 43},
            {"Standartization", 32},
            {"Economy", 24},
            {"Blogs", 44},
            {"Simplicity", 78},
            {"Social Software", 76},
            {"CSS", 65},
            {"Collaboration", 12},
            {"Audio", 53},
            {"XML", 9},
            {"Atom", 4},
            {"SOAP", 5},
            {"SVG", 51},
            {"Web 2.0", 100},
            { "CSV", 89},
            {"Map Reduce", 87},
            {"OpenID", 31},
            {"Trust", 5},
            {"Ruby on Rails",21},
            {"Widgets", 32},
            {"SEO", 1},
            {"XHTML", 3},
            {"я устал", 3},
            {"Browser", 34},
            {"Pay Per Click", 49},
            {"Aggregators", 25},
            {"AJAX", 37},
            {"The Long Tail", 61},
            {"VC", 2},
            {"Semantic", 22},
            {"Python", 2}

        };
        private readonly Cloud cloud = new Cloud(tagsDictionary);

        public CloudForm()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(0, 34, 43);   
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var center = new Point(300, 300);
            var layouter = new CircularCloudLayouter(center);

            foreach (var tag in cloud.Tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                tag.Rectangle = layouter.PutNextRectangle(tagSize);
                graphics.DrawString(tag.Text, tag.Font, tag.Brush, tag.Rectangle.Location);
            }  
        }
    }
}
