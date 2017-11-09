using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TagsCloudVisualization.Architecture.TagTypes;

namespace TagsCloudVisualization
{
    class Cloud
    {
        public Point Center { get; }
        public Cloud(Point center)
        {
            Center = center;
        }

        public List<Tag> MakeTagsFromTuples(List<(string, int)> pairs)
        {
            var tags = new List<Tag>();
            var fifteenPercent = (int)(pairs.Count * 0.15);
            var thirtyFivePercent = (int)(pairs.Count * 0.35);
            
            tags.Add(new BiggestTag() { Text = pairs.First().Item1 });

            tags.AddRange(pairs
                .Skip(1)
                .Take(fifteenPercent)
                .Select(e => new BigTag() { Text = e.Item1})
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent)
                .Take(thirtyFivePercent)
                .Select(e => new MediumTag() { Text = e.Item1 })
                .ToList());

            tags.AddRange(pairs
                .Skip(1 + fifteenPercent + thirtyFivePercent)
                .Select(e => new SmallTag() { Text = e.Item1 })
                .ToList());

            return tags;
        }

        public void SetRectangeForEachTag(List<Tag> tags)
        {
            var layouter = new CircularCloudLayouter(Center);
            foreach (var tag in tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                tag.Rectangle = layouter.PutNextRectangle(tagSize);
            }
        }
    }
}
