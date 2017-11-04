using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter
    {
        public Vector CloudCenter { get; set; }
        public List<Rectangle> Rectangles = new List<Rectangle>();

        public CircularCloudLayouter(Point center)
        {
            CloudCenter = new Vector(center);
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var rectangleVector = Rectangles.Count == 0
                ? CloudCenter - new Vector(rectangleSize.Width, rectangleSize.Height) / 2
                : GetRectangleVector(rectangleSize);
            var rectangle = new Rectangle(rectangleVector.X, rectangleVector.Y, rectangleSize.Width,
                rectangleSize.Height);
            Rectangles.Add(rectangle);
            return rectangle;
        }

        public Vector GetRectangleVector(Size rectangleSize)
        {
            var radius = Math.Min(Rectangles.First().Width, Rectangles.First().Height);
            var step = 1;
            while (true)
            {
                for (int offsetX = -radius; offsetX < radius; offsetX++)
                {
                    var offsetY = (int)Math.Round(Math.Sqrt(radius * radius - offsetX * offsetX));
                    var rectangleVector1 = CloudCenter - new Vector(offsetX, offsetY) / 2;
                    var rectangleVector2 = CloudCenter - new Vector(offsetX, -offsetY) / 2;
                    if (CouldPutRectangle(rectangleVector1, rectangleSize))
                        return rectangleVector1;
                    if (CouldPutRectangle(rectangleVector2, rectangleSize))
                        return rectangleVector2;
                }
                radius += step;
            }
        }

        private bool CouldPutRectangle(Vector rectangleVector, Size rectangleSize)
        {
            if (rectangleVector.X < 0 || rectangleVector.Y < 0)
                return false;
            var potentialRectangle = new Rectangle(rectangleVector.ToPoint(), rectangleSize);
            foreach (var rectangle in Rectangles)
                if (rectangle.IntersectsWith(potentialRectangle))
                    return false;
            return true;
        }
    }
}