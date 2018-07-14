using System;

namespace TimeSeriesSpotter.Dto
{
    /// <summary>
    /// Class to represent a single data point from datastream
    /// </summary>
    public class DataItemDto
    {
        public DateTime Date { get; set; }
        public Decimal Value { get; set; }
        public bool? IsOutlier { get; set; }
        public double Score { get; set; }
    }
}
