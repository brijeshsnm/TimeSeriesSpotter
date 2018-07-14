using System;
using NUnit.Framework;
using TimeSeriesSpotter.DataPublisher;

namespace TimeSeriesSpotter.Test.DataPublisher
{
    [TestFixture]
    public class FileFeedPublisherTest
    {
        [Test]
        public void ParseDataItem()
        {
            var fileParser = new FileFeedPublisher(System.IO.Path.GetTempFileName());
            var parsedItem1 = fileParser.ParseDataItem("09/01/1990,10.4577362");
            var parsedItem2 = fileParser.ParseDataItem("10/01/1990, 1000.7628687");
            var parsedItem3 = fileParser.ParseDataItem("1/5/1995, 10000.99");

            Assert.AreEqual(parsedItem1.Date,new DateTime(1990, 1, 9));
            Assert.AreEqual(parsedItem2.Date, new DateTime(1990, 1, 10));
            Assert.AreEqual(parsedItem3.Date, new DateTime(1995, 5, 1));

            Assert.AreEqual(parsedItem1.Value, 10.4577362);
            Assert.AreEqual(parsedItem2.Value, 1000.7628687);
            Assert.AreEqual(parsedItem3.Value, 10000.99);

        }
    }
}
