﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter circularCloudLayouter;

        [SetUp] 
        public void Initialize()
        {
            circularCloudLayouter = new CircularCloudLayouter(new Point(50, 50));
        }

        //[Test]
        //public void DoSomething_WhenSomething()
        //{
        //    var circularCloudLayouter = new CircularCloudLayouter(new Point(50, 50));
        //    circularCloudLayouter.PutNextRectangle(new Size(5, 5));
        //    circularCloudLayouter.PutNextRectangle(new Size(2, 5));
        //    circularCloudLayouter.PutNextRectangle(new Size(5, 2));
        //    circularCloudLayouter.PutNextRectangle(new Size(3, 3));
        //    circularCloudLayouter.PutNextRectangle(new Size(4, 4));
        //}

        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(-10, -10)]
        public void CircularCloudLayouter_ThrowsException(int x, int y)
        {
            Action create = () => new CircularCloudLayouter(new Point(x, y));
            create.Should().Throw<ArgumentException>();
        }

        [TestCase(-10, 10)]
        [TestCase(10, -10)]
        [TestCase(-10, -10)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        [TestCase(0, 0)]
        public void PutNextRectangle_ThrowsException(int width, int height)
        {
            Action create = () => circularCloudLayouter.PutNextRectangle(new Size(width, height));
            create.Should().Throw<ArgumentException>();
        }

        [Test]
        public void CircularCloudLayouter_DoesNotContainsAnyRectangles_AfterCreating()
        {
            circularCloudLayouter.GetRectangles().Should().BeEmpty();
        }

        [Test]
        public void CircularCloudLayouter_ContainsManyRectangles_AfterAdding()
        {
            for (var i = 0; i < 10; i++)
                circularCloudLayouter.PutNextRectangle(new Size(10, 10));
            circularCloudLayouter.GetRectangles().Should().HaveCount(10);
        }

        [Test]
        public void CircularCloudLayouter_ContainsCorrectRectangle_AfterAdding()
        {
            circularCloudLayouter.PutNextRectangle(new Size(10, 10));
            circularCloudLayouter.GetRectangles()[0].Size.Should().Be(new Size(10, 10));
        }

        [Test]
        public void Rectangles_DoNotIntersectEachOther()
        {
            for (var i = 0; i < 10; i++)
                circularCloudLayouter.PutNextRectangle(new Size(5, 2));

            var rectangles = circularCloudLayouter.GetRectangles();
            for (var j = 0; j < 9; j++)
            for (var i = j + 1; i < 10; i++)
            {
                rectangles[j].IntersectsWith(rectangles[i]).Should().BeFalse();
            }
        }
    }
}
