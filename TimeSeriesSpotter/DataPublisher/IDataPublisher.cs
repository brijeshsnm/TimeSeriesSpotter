using System;
using TimeSeriesSpotter.Dto;

namespace TimeSeriesSpotter.DataPublisher
{
    public interface IDataPublisher
    {
        /// <summary>
        /// Fetch data from data source like file, webapi, database, message bus... and convert them into IObservable item
        /// </summary>
        /// <returns></returns>
        IObservable<DataItemDto> Fetch();
    }
}
