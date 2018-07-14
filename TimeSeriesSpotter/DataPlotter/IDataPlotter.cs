using System.Collections.Generic;
using TimeSeriesSpotter.Dto;

namespace TimeSeriesSpotter.DataPlotter
{
    /// <summary>
    /// Dump data in output medium
    /// </summary>
    public interface IDataPlotter
    {
        void Plot(IEnumerable<DataItemDto> data);
    }
}
