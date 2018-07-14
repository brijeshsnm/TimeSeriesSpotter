using System;
using System.Collections.Generic;
using System.Linq;
using TimeSeriesSpotter.Dto;
using TimeSeriesSpotter.Helper;

namespace TimeSeriesSpotter.OutlierCalculator
{
    /// <summary>
    /// Uses Z Score to determine outlier, more details on Z-Score could be found at http://www.statisticshowto.com/probability-and-statistics/z-score/
    /// </summary>
    public class ZScoreDeviation: IOutlierCalculator
    {
        private readonly Queue<DataItemDto> _trainingData;
        private readonly int _trainingDataSize;
        private readonly double _outlierThreshHold;
        private readonly StatsHelper _statsHelper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainingDataSize">Window size to calculate running average and standardeviation</param>
        /// <param name="outlierThreshHold">Threshold to determine if point is outlier</param>
        /// <param name="statsHelper">Helper class to do statistical calculation</param>
        public ZScoreDeviation(int trainingDataSize, double outlierThreshHold, StatsHelper statsHelper)
        {
            _trainingDataSize = trainingDataSize;
            _outlierThreshHold = outlierThreshHold;
            _statsHelper = statsHelper;
            _trainingData = new Queue<DataItemDto>();
        }

        /// <summary>
        /// Populate DataItemDto.Score and DataItemDto.IsOutlier
        /// </summary>
        /// <param name="input"></param>
        public void CalculateOutlier(IEnumerable<DataItemDto> input)
        {
            foreach (var item in input)
            { 
               _trainingData.Enqueue(item);
                if (_trainingData.Count > _trainingDataSize)
                {
                    _trainingData.Dequeue();
                }
            }

            var mean = _statsHelper.CalculateMean(_trainingData.Select(x=>(double)x.Value));
            var std = _statsHelper.CalculateStandardDeviation(_trainingData.Select(x => (double)x.Value));

            foreach (var item in input)
            {
                item.Score = _statsHelper.CalculateZScore((double)item.Value, mean, std); 
                item.IsOutlier = Math.Abs(item.Score) >= _outlierThreshHold;
            }

        }

    }
}
