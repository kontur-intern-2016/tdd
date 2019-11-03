﻿using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class Spriral_Tests
    {
        public Spiral spiral;
        [SetUp]
        public void SetUp()
        {
            spiral = new Spiral(new Point(100, 100));
        }
        
        [Test]
        public void SpiralCtor_ValidParameter_ShouldNotThrowException()
        {
            Action act = () => new Spiral(new Point(0, 0));
            act.Should().NotThrow();
        }

        [Test]
        public void GetNextPoint_ShouldNotThrowException()
        {
            Action act = () => new Spiral(new Point(0, 0)).GetNextPoint();
            act.Should().NotThrow();
        }

        [Test]
        public void GetNextPoint_SpiralRadius_ShouldBeGreaterThanBefore()
        {
            var previousRadius = spiral.radius;
            spiral.GetNextPoint();
            var currentRadius = spiral.radius;
            var difference = currentRadius - previousRadius;
            difference.Should().BePositive();
        }

        [Test]
        public void GetNextPoint_SpiralAngle_ShouldBeGreaterThanBefore()
        {
            var previousAngle = spiral.angle;
            spiral.GetNextPoint();
            var currentAngle = spiral.angle;
            var difference = currentAngle - previousAngle;
            difference.Should().BePositive();
        }
    }
}