using System;
using System.Collections.Generic;
using System.Linq;


namespace TimeSeriesSpotter.Helper
{
    public class StatsHelper
    {
        public virtual double CalculateMean(IEnumerable<double> input)
        {
            return input.Average();
        }

        public virtual double CalculateStandardDeviation(IEnumerable<double> input)
        {
            var avg = CalculateMean(input);
            return Math.Sqrt(input.Average(v => Math.Pow(v - avg, 2)));
        }

        public virtual double CalculateZScore(double value, double mean, double standardDeviation)
        {
            return (value - mean) / standardDeviation;
        }
    }
}
