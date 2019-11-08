﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class ArchimedeanSpiral
    {
        private const double TwoPi = 2 * Math.PI;

        private readonly Point center;

        public ArchimedeanSpiral(Point center)
        {
            this.center = center;
        }

        /// <summary>
        /// Генерирует точки из которых состоит спираль
        /// </summary>
        /// <param name="numberOfTurns">Количество витков спирали</param>
        /// <param name="deltaAngle">Значение угла, на которое будет происходит смещение каждую итерацию</param>
        /// <param name="step">Расстояние между витками</param>
        /// <returns></returns>
        public IEnumerable<Point> GetPoints(int numberOfTurns, double deltaAngle = TwoPi / 360, int step = 20)
        {
            for (var angle = 0.0; angle < numberOfTurns * Math.PI; angle += deltaAngle)
            {
                yield return new Point(
                    (int) (step * angle * Math.Cos(angle) / TwoPi) + center.X,
                    (int) (step * angle * Math.Sin(angle) / TwoPi) + center.Y
                );
            }
        }

    }
}