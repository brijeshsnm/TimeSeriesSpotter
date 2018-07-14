using System.Collections.Generic;
using System.Linq;
using TimeSeriesSpotter.Dto;
using System.IO;

namespace TimeSeriesSpotter.DataPlotter
{
    /// <summary>
    /// Dumps data into CSV file
    /// </summary>
    public class CsvPlotter: IDataPlotter
    {
        private readonly string _fileName;
        private readonly bool _appendMode;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">Output file name</param>
        /// <param name="appendMode">Set to true if append to existing file is required</param>
        public CsvPlotter(string fileName, bool appendMode = true)
        {
            _fileName = fileName;
            _appendMode = appendMode;
            var dirPath = Path.GetDirectoryName(_fileName);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            if (File.Exists(_fileName))
            {
                File.Delete(_fileName);
            }
        }

        /// <summary>
        /// Writes DataItemDto.Date and DataItemDto.Value in csv file
        /// </summary>
        /// <param name="data"> data items to write to file</param>
        public void Plot(IEnumerable<DataItemDto> data)
        {
            if (_appendMode)
            {
                File.AppendAllLines(_fileName, data.Select(x => $"{x.Date:dd/MM/yyyy}, {x.Value}"));
            }
            else
            {
                File.WriteAllLines(_fileName, data.Select(x => $"{x.Date:dd/MM/yyyy}, {x.Value}"));
            }


        }
    }
}
