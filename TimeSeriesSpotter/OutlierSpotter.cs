using System;
using System.Linq;
using System.Reactive.Linq;
using NLog;
using TimeSeriesSpotter.DataPlotter;
using TimeSeriesSpotter.DataPublisher;
using TimeSeriesSpotter.OutlierCalculator;

namespace TimeSeriesSpotter
{
    /// <summary>
    /// Class to scan input datsource for outlier and dump to output 
    /// </summary>
    public class OutlierSpotter
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly IOutlierCalculator _outlierCalculator;
        private readonly IDataPlotter _plotter;
        private readonly IDataPublisher _source;
        private readonly int _bufferSize;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outlierCalculator">Algorithm to calcuate outlier</param>
        /// <param name="source">Input data source</param>
        /// <param name="plotter">Output data source</param>
        /// <param name="bufferSize">Data items to hold before processing</param>
        public OutlierSpotter(IOutlierCalculator outlierCalculator, IDataPublisher source, IDataPlotter plotter, int bufferSize)
        {
            _outlierCalculator = outlierCalculator;
            _plotter = plotter;
            _source = source;
            _bufferSize = bufferSize;
        }

        public void Run()
        {
            _source.Fetch().Buffer(_bufferSize).Subscribe(buffer =>
            {
                _outlierCalculator.CalculateOutlier(buffer);
                var outlierItems = buffer.Where(x => x.IsOutlier == true);
                foreach (var outlierItem in outlierItems)
                {
                    Logger.Info("Outlier detected at {0} {1}", outlierItem.Date, outlierItem.Value);
                }
                _plotter.Plot(buffer.Where(x => x.IsOutlier == false));
            });
        }


    }
}
