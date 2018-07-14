using System.Collections.Generic;
using TimeSeriesSpotter.Dto;

namespace TimeSeriesSpotter.OutlierCalculator
{
    public interface IOutlierCalculator
    {
        void CalculateOutlier(IEnumerable<DataItemDto> input);
    }
}
