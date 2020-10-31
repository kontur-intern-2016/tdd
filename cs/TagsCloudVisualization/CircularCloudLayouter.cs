﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TagsCloudVisualization
{
    public class CircularCloudLayouter : ICloudLayouter
    {
        private readonly Point center;
        private readonly List<Rectangle> currentRectangles;
        private int currentRadius;
        private double currentAngle;
        public CircularCloudLayouter(Point center)
        {
            this.center = center;
            currentRectangles = new List<Rectangle>();
            currentRadius = 0;
            currentAngle = 0;
        }
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            var point = GetNextPoint();
            if (point == center)
            {
                currentRectangles.Add(new Rectangle(
                    new Point(
                        center.X - rectangleSize.Width / 2,
                        center.Y - rectangleSize.Height / 2),
                    rectangleSize));
                return currentRectangles[0];
            }
            var newRectangle = new Rectangle(point, rectangleSize);
            while(IntersectWithPreviousRectangles(newRectangle))
            {
                point = GetNextPoint();
                newRectangle = new Rectangle(point, rectangleSize);
            }
            currentRectangles.Add(newRectangle);
            return newRectangle;
        }

        private Point GetNextPoint()
        {
            if (currentRadius == 0)
            {
                currentRadius++;
                return center;
            }
            var point = new Point(
                (int)(center.X + currentRadius * Math.Cos(currentAngle)),
                (int)(center.Y + currentRadius * Math.Sin(currentAngle)));
            currentAngle += Math.PI / (6 + currentRadius);
            if (Math.Abs(currentAngle - 2 * Math.PI) < 0.0000001)
            {
                currentAngle = 0;
                currentRadius++;
            }
            return point;
        }

        private bool IntersectWithPreviousRectangles(Rectangle newRectangle)
        {
            foreach (var rectangle in currentRectangles)
            {
                if (newRectangle.IntersectsWith(rectangle))
                    return true;
            }
            return false;
        }
    }
}
