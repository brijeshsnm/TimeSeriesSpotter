using System;
using System.Collections.Generic;
using NUnit.Framework;
using TimeSeriesSpotter.Helper;

namespace TimeSeriesSpotter.Test.Helper
{
    [TestFixture]
    public class StatsHelperTest
    {
        [Test]
        public void CalculateMean()
        {
            var statsHelper = new StatsHelper();
            var mean = statsHelper.CalculateMean(new List<double> {5, 10, 15, 20, 25});
            Assert.AreEqual(mean, 15);
        }

        [Test]
        public void CalculateStandardDeviation()
        {
            var statsHelper = new StatsHelper();
            var std = statsHelper.CalculateStandardDeviation(new List<double> { 10, 20, 38, 23, 38, 23, 21 });
            Assert.AreEqual(Math.Round(std), 9);
        }

        [Test]
        public void CalculateZScore()
        {
            var statsHelper = new StatsHelper();
            var zScore = statsHelper.CalculateZScore(55, 35, 5);
            Assert.AreEqual(zScore, 4);
        }


    }
}
