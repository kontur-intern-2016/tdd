﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    class Program
    {
        static void Main()
        {
            var layout = new CircularCloudLayouter(new Point(200, 200));
            var random = new Random();
            for (int i = 1; i < 50; i++)
                layout.PutNextRectangle(new Size(random.Next(30, 60), random.Next(20, 60)));
            var c = new CircularCloudVisualization(layout);
            c.SaveImage("Test");
        }
    }
}
