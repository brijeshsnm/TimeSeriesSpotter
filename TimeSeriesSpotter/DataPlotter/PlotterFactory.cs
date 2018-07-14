using System.Configuration;

namespace TimeSeriesSpotter.DataPlotter
{

    /// <summary>
    /// Factory to create output plotter
    /// </summary>
    public class PlotterFactory
    {
        public IDataPlotter Create(PlotterType plotterType)
        {
            if (plotterType == PlotterType.Csv)
            {
                var outFileName = ConfigurationManager.AppSettings["outputFileName"];
                return new CsvPlotter(outFileName);
            }

            return null;
        }
    }
}
