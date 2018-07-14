using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TimeSeriesSpotter.Dto;
using TimeSeriesSpotter.Helper;
using TimeSeriesSpotter.OutlierCalculator;

namespace TimeSeriesSpotter.Test.OutlierCalculator
{
    [TestFixture]
    public class ZScoreDeviationTest
    {
        [Test]
        public void CalculateOutlier_MarksAllOutlier_True()
        {
            var statsHelperMoq = new Mock<StatsHelper>();

            var testItems = new List<DataItemDto> { new DataItemDto { Date = DateTime.Today, Value = 10 }, new DataItemDto { Date = DateTime.Today, Value = 10 }, new DataItemDto { Date = DateTime.Today, Value = 10 } };

            statsHelperMoq.Setup(x => x.CalculateMean(It.IsAny<IEnumerable<Double>>()))
                .Returns(5);
            statsHelperMoq.Setup(x => x.CalculateStandardDeviation(It.IsAny<IEnumerable<Double>>()))
                .Returns(2);
            statsHelperMoq.Setup(x => x.CalculateZScore(10, 5, 2))
                .Returns(5);

            var zScoreDeviation = new ZScoreDeviation(10, 3, statsHelperMoq.Object);
           
            zScoreDeviation.CalculateOutlier(testItems);

            Assert.IsTrue(testItems.All(x=>x.IsOutlier == true));


        }

        [Test]
        public void CalculateOutlier_MarksAllOutlier_False()
        {
            var statsHelperMoq = new Mock<StatsHelper>();

            var testItems = new List<DataItemDto> { new DataItemDto { Date = DateTime.Today, Value = 10 }, new DataItemDto { Date = DateTime.Today, Value = 10 }, new DataItemDto { Date = DateTime.Today, Value = 10 } };

            statsHelperMoq.Setup(x => x.CalculateMean(It.IsAny<IEnumerable<Double>>()))
                .Returns(5);
            statsHelperMoq.Setup(x => x.CalculateStandardDeviation(It.IsAny<IEnumerable<Double>>()))
                .Returns(2);
            statsHelperMoq.Setup(x => x.CalculateZScore(10, 5, 2))
                .Returns(5);

            var zScoreDeviation = new ZScoreDeviation(10, 10, statsHelperMoq.Object);

            zScoreDeviation.CalculateOutlier(testItems);

            Assert.IsTrue(testItems.All(x => x.IsOutlier == false));
        }


    }
}
