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
        public List<Tag> Tags = new List<Tag>();
        public Point Center { get; }
        public Cloud(Point center, List<Tuple<string, int>> tags)
        {
            Center = center;
            MakeTagsFromTuples(tags);
            MakeLayout();
        }

        private void MakeTagsFromTuples(List<Tuple<string, int>> pairs)
        {
            var fifteenPercent = (int)(pairs.Count * 0.15);
            var thirtyFivePercent = (int)(pairs.Count * 0.35);

            Tags.Add(new BiggestTag() { Text = pairs.First().Item1 });

            Tags.AddRange(pairs
                .Skip(1)
                .Take(fifteenPercent)
                .Select(e => new BigTag() { Text = e.Item1})
                .ToList());

            Tags.AddRange(pairs
                .Skip(1 + fifteenPercent)
                .Take(thirtyFivePercent)
                .Select(e => new MediumTag() { Text = e.Item1 })
                .ToList());

            Tags.AddRange(pairs
                .Skip(1 + fifteenPercent + thirtyFivePercent)
                .Select(e => new SmallTag() { Text = e.Item1 })
                .ToList());
        }

        public void MakeLayout()
        {
            var layouter = new CircularCloudLayouter(Center);
            foreach (var tag in Tags)
            {
                var tagSize = TextRenderer.MeasureText(tag.Text, tag.Font);
                tag.Rectangle = layouter.PutNextRectangle(tagSize);
            }
        }
    }
}
