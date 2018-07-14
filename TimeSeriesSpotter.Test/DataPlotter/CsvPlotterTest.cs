using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using TimeSeriesSpotter.DataPlotter;
using TimeSeriesSpotter.Dto;

namespace TimeSeriesSpotter.Test.DataPlotter
{
    [TestFixture]
    public class CsvPlotterTest
    {
        [Test]
        public void Plot_Correct_Format()
        {
            var fileName = Path.GetTempFileName();
            var csvPlotter = new CsvPlotter(fileName, true);
            var testItems = new List<DataItemDto>
            {
                new DataItemDto {Date = DateTime.Today, Value = 10},
                new DataItemDto {Date = DateTime.Today.AddDays(1), Value = 10}
            };
            csvPlotter.Plot(testItems);
            var fileOutput = File.ReadAllText(fileName);
            var expectedOutput = @"02/07/2018, 10
03/07/2018, 10
";
            Assert.AreEqual(fileOutput, expectedOutput);
            File.Delete(fileName);
        }

        [Test]
        public void Plot_Append_Mode_True()
        {
            var fileName = Path.GetTempFileName();
            var csvPlotter = new CsvPlotter(fileName, true);
            var testItems = new List<DataItemDto>
            {
                new DataItemDto {Date = DateTime.Today, Value = 10},
                new DataItemDto {Date = DateTime.Today.AddDays(1), Value = 10}
            };
            csvPlotter.Plot(testItems);
            csvPlotter.Plot(testItems);
            var lines = File.ReadAllLines(fileName);

            Assert.AreEqual(lines.Length, 4);
            File.Delete(fileName);
        }


        [Test]
        public void Plot_Append_Mode_False()
        {
            var fileName = Path.GetTempFileName();
            var csvPlotter = new CsvPlotter(fileName, false);
            var testItems = new List<DataItemDto>
            {
                new DataItemDto {Date = DateTime.Today, Value = 10},
                new DataItemDto {Date = DateTime.Today.AddDays(1), Value = 10}
            };
            csvPlotter.Plot(testItems);
            csvPlotter.Plot(testItems);
            var lines = File.ReadAllLines(fileName);

            Assert.AreEqual(lines.Length, 2);
            File.Delete(fileName);
        }
    }
}
