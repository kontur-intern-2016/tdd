﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    internal class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private Point center;
        private readonly Random rnd = new Random();
        private const int minHeight = 6;
        private const int maxHeight = 50;
        private const int maxWidthHeightRatio = 10;

        [SetUp]
        public void SetUp()
        {
            center = new Point(120,150);
            layouter = new CircularCloudLayouter(center);
        }

        [Test]
        public void HaveCorrectCenter()
        {
            layouter.Center.Should().BeEquivalentTo(center);
        }

        [Test]
        public void MakeRectangleWithCorrectSize()
        {
            var size = RandomSize();
            layouter.PutNextRectangle(size).Size.Should().BeEquivalentTo(size);
        }

        [Test]
        public void PutFirstRectangleCenterOnLayoutCenter()
        {
            var rect = layouter.PutNextRectangle(RandomSize());
            rect.Center().Should().BeEquivalentTo(center);
        }

        [Test]
        public void MakeManyCorrectRectangles()
        {
            const int count = 100;
            var sizes = count.Times(RandomSize).ToArray();
            sizes.Select(layouter.PutNextRectangle).Select(x => x.Size).Should().BeEquivalentTo(sizes);
        }

        private Size RandomSize()
        {
            var height = rnd.Next(minHeight, maxHeight);
            var width = rnd.Next(height, height* maxWidthHeightRatio);
            return new Size(width, height);
        }

        private Rectangle[] GenerateRectangles(int count)=>
            count.Times(RandomSize).Select(layouter.PutNextRectangle).ToArray();

        [Test]  
        public void DoNotOverlapManyRectangles()
        {
            const int count = 100;
            var rectangles = GenerateRectangles(count);

            for (int i = 0; i < count; i++) 
            for (int j = 0; j < i; j++)
                Assert.False(rectangles[i].IntersectsWith(rectangles[j]),
                    $"{i}th rectangle was {rectangles[i]}, and {j}th rectangle was{rectangles[j]}");
        }

        [Test]
        public void FitManySameSizesInto2TimesBiggerCircle()
        {
            const int count = 100;
            var size = new Size(24,120);
            //var space = sizes.Aggregate(0, (sum, size) => sum + size.Space());
            var space = size.Space() * count;
            var radius = Math.Sqrt(2 * space / Math.PI);
            
            var rects = count.Times(()=>layouter.PutNextRectangle(size));
            
            rects.SelectMany(x => x.Points())
                .Select(x => x.DistanceTo(center))
                .All(x => x < radius)
                .Should().BeTrue();
        }
        
    }
}
