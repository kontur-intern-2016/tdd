﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Visualizer
    {
        public Color BackColor { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }

        public Visualizer(Color fillColor, Color borderColor, Color backColor)
        {
            BackColor = backColor;
            FillColor = fillColor;
            BorderColor = borderColor;
        }

        public Bitmap RenderToBitmap(IEnumerable<Rectangle> rectangles, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(BackColor), 0, 0, width, height);
            foreach (var r in rectangles)
            {
                graphics.FillRectangle(new SolidBrush(FillColor), r);
                graphics.DrawRectangle(new Pen(BorderColor), r);
            }

            return bitmap;
        }
    }
}
