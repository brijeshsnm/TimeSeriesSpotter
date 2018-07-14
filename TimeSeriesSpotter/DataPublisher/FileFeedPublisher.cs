using System;
using System.Globalization;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using TimeSeriesSpotter.Dto;


namespace TimeSeriesSpotter.DataPublisher
{
    /// <summary>
    /// Read data feed from a file and publish all items
    /// </summary>
    public class FileFeedPublisher : IDataPublisher
    {
        private readonly string _fileName;

        public FileFeedPublisher(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Parse string into DataItemDto object
        /// </summary>
        /// <param name="text">Text in format "d/M/yyyy,123.0" </param>
        /// <returns>Text parsed into DataItemDto object</returns>
        public DataItemDto ParseDataItem(string text)
        {
            var lineItems = text.Split(',');
            var dateStr = lineItems[0];
            var valStr = lineItems[1];
            var dataItemDto = new DataItemDto
            {
                Date = DateTime.ParseExact(dateStr, "d/M/yyyy", CultureInfo.InvariantCulture),
                Value = Decimal.Parse(valStr)
            };
            return dataItemDto;
        }

        /// <summary>
        /// Read file and convert data feed into IObservable stream
        /// </summary>
        /// <returns></returns>
        public IObservable<DataItemDto> Fetch()
        {
            return Observable.Create<DataItemDto>(
                observer =>
                {
                    bool firstLineRead = false;
                    foreach (var line in File.ReadLines(_fileName))
                    {
                        if (!firstLineRead)
                        {
                            firstLineRead = true;
                            continue;
                        }
                        var number = ParseDataItem(line);
                        observer.OnNext(number);
                    }
                    observer.OnCompleted();
                    return Disposable.Empty;
                });
        }
    }
}
