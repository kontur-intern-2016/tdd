﻿using System;
using System.Drawing;

namespace ProjectCircularCloudLayouter
{
    public class ArchimedeanSpiral : ISpiral
    {
        private double _spiralAngle;
        private const double SpiralStep = 0.5;
        public Point Center { get; }

        public ArchimedeanSpiral(Point center)
        {
            Center = center;
        }

        public Point GetNewSpiralPoint()
        {
            var position = new Point((int) Math.Round(Center.X + SpiralStep * _spiralAngle * Math.Cos(_spiralAngle)),
                (int) Math.Round(Center.Y + SpiralStep * _spiralAngle * Math.Sin(_spiralAngle)));
            _spiralAngle += 0.017;
            return position;
            //еще варинат формулы, когда x и y находить через: Center.X + Math.Sqrt(_spiralAngle) * Math.Cos(_spiralAngle)
            //в таком случае плотность будет больше, но меньше скорость
        }
    }
}