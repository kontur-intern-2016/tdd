﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    class CircularCloudLayouter
    {
        public Point Center { get; set; }
        private readonly List<Rectangle> placedRectangles = new List<Rectangle>();
        private readonly double shiftOnSpiral = 0.01;
        private int rotationAngle = 0;

        public CircularCloudLayouter(Point center) => Center = center;

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            CheckRectangleSizeCorrectness(rectangleSize);

            var possibleNextRectangle = GetNextPossibleRectangle(rectangleSize);
            while (placedRectangles.Any(r => r.IntersectsWith(possibleNextRectangle)))
                possibleNextRectangle = GetNextPossibleRectangle(rectangleSize);

            placedRectangles.Add(possibleNextRectangle);

            return possibleNextRectangle;
        }

        private Rectangle GetNextPossibleRectangle(Size rectangleSize) => new Rectangle(GetNextPointOnSpiral(), rectangleSize);

        private Point GetNextPointOnSpiral()
        {
            var dx = Math.Cos(rotationAngle) * rotationAngle * shiftOnSpiral;
            var dy = Math.Sin(rotationAngle) * rotationAngle * shiftOnSpiral;

            rotationAngle += 1;

            var nextX = Center.X + (int) dx;
            var nextY = Center.Y + (int) dy;

            return new Point(nextX, nextY);
        }

        private static void CheckRectangleSizeCorrectness(Size rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException();
        }
    }
}
