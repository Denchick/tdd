using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization
{
    class Program
    {
        public static void Main()
        {
            var textFromFile = GetTextFromFile(@"text.txt");
            var center = new Point(300, 300);
            var cloud = new Cloud(center, GetMostFrequentWords(textFromFile, 70));

            var leftBorder = cloud.Tags
                .Select(e => e.Rectangle.Left)
                .Min();
            var rightBorder = cloud.Tags
                .Select(e => e.Rectangle.Right)
                .Max();
            var topBorder = cloud.Tags
                .Select(e => e.Rectangle.Top)
                .Min();
            var bottomBorder = cloud.Tags
                .Select(e => e.Rectangle.Bottom)
                .Max();

            var size = new Size(rightBorder - leftBorder, bottomBorder - topBorder); 
            var offset = new Point(leftBorder, topBorder);

            var bitmap = new Bitmap(size.Width, size.Height);
            var tagsDrawer = new TagsDrawer(bitmap, cloud.Tags, offset);
            tagsDrawer.DrawTags();
            bitmap.Save("image.bmp");
        }

        public static List<Tuple<string, int>> GetMostFrequentWords(string text, int count)
        {
            return Regex.Split(text.ToLower(), @"\W+")
                .Where(word => word != "" && word.Length > 2)
                .GroupBy(word => word)
                .Select(group => Tuple.Create(ToTitleCase(group.Key), group.Count()))
                .OrderByDescending(tuple => tuple.Item2)
                .ThenBy(tuple => tuple.Item1)
                .Take(count)
                .ToList();
        }

        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static string GetTextFromFile(string filename)
        {
            string textFromFile;
            using (FileStream fstream = File.OpenRead(filename))
            {
                byte[] array = new byte[fstream.Length];
                fstream.Read(array, 0, array.Length);
                textFromFile = System.Text.Encoding.UTF8.GetString(array);
            }
            return textFromFile;
        }
    }
}
