using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TagsCloudVisualization
{
    class Program
    {
        public static void Main()
        {
            var textFromFile = File.ReadAllText(@"text.txt");
            var center = new Point(300, 300);
            var cloud = new Cloud(center);
            var tags = cloud.MakeTagsFromTuples(GetMostFrequentWords(textFromFile, 70));
            cloud.SetEveryTagRectange(tags);

            var leftBorder = tags
                .Select(e => e.Rectangle.Left)
                .Min();
            var rightBorder = tags
                .Select(e => e.Rectangle.Right)
                .Max();
            var topBorder = tags
                .Select(e => e.Rectangle.Top)
                .Min();
            var bottomBorder = tags
                .Select(e => e.Rectangle.Bottom)
                .Max();

            var size = new Size(rightBorder - leftBorder, bottomBorder - topBorder); 
            var offset = new Point(leftBorder, topBorder);

            var tagsDrawer = new TagsDrawer("image.bmp", tags, offset, size);
            
            
        }

        public static List<(string, int)> GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text.ToLower(), @"\W+")
                .Where(word => !string.IsNullOrWhiteSpace(word) && word.Length > 2)
                .GroupBy(word => word)
                .Select(group => (ToTitleCase(group.Key), group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(count)
                .ToList();
        }

        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}
