using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.Architecture.TagTypes;

namespace TagsCloudVisualization
{
    class Cloud
    {
        public List<Tag> Tags = new List<Tag>();

        public Cloud(Dictionary<string, int> tagsDictionary)
        {
            var pairs = tagsDictionary
                .Select(e => Tuple.Create(e.Key, e.Value))
                .OrderByDescending(e => e.Item2)
                .ToList();
            MakeTagsFromTuples(pairs);
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
    }
}
