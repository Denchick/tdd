using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows;

namespace TagsCloudVisualization
{
    public class TagsDrawer
    {
        private List<Tag> Tags { get; }
        private Bitmap Bitmap { get; }
        private Point Offset { get; }
        
        public TagsDrawer(Bitmap bitmap, List<Tag> tags, Point offset)
        {
            Bitmap = bitmap;
            Tags = tags;
            Offset = offset;
            var graphics = Graphics.FromImage(Bitmap);
            graphics.FillRectangle(Brushes.Black, 0, 0, Bitmap.Width, Bitmap.Height);   
        }


        public void DrawTags()
        {
            var graphics = Graphics.FromImage(Bitmap);

            foreach (var tag in Tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                var sourceRectangle = tag.Rectangle;
                tag.Rectangle =
                    new Rectangle(new Point(sourceRectangle.Left - Offset.X, sourceRectangle.Top - Offset.Y),
                        sourceRectangle.Size);
                graphics.DrawString(tag.Text, tag.Font, tag.Brush, tag.Rectangle.Location);
            }
        }
    }
}
