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
            var mostFrequentWords = GetMostFrequentWords(textFromFile, 70);

            var cloudCenter = new Point(300, 300);
            var layouter = new CircularCloudLayouter(cloudCenter);

            var tags = layouter.MakeTagsFromTuples(mostFrequentWords);
            layouter.SetRectangeForEachTag(tags);

            var tagsDrawer = new TagsDrawer("image.bmp", tags);
        }

        public static List<(string, int)> GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text.ToLower(), @"\W+")
                .Where(word => !string.IsNullOrWhiteSpace(word) && word.Length > 2)
                .GroupBy(word => word)
                .Select(group => (group.Key.CapitalizeFirstLetter(), group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(count)
                .ToList();
        }
    }
}
