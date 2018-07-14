using System;
using NLog;
using TimeSeriesSpotter.DataPlotter;
using TimeSeriesSpotter.DataPublisher;
using System.Configuration;
using System.IO;
using TimeSeriesSpotter.OutlierCalculator;

namespace TimeSeriesSpotter
{
    public class Program
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Logger.Info("Input file = {0} ", Path.GetFullPath(ConfigurationManager.AppSettings["inputFileName"]));

                var publisherFactory = new DataPublisherFactory();
                var inputDataPublisher = publisherFactory.Create(PublisherType.File);

                var outputPlotterFactory = new PlotterFactory();
                var outputPlotter = outputPlotterFactory.Create(PlotterType.Csv);

                var outlierCalcFactory = new OutlierCalcFactory();
                var outlierAlgo = outlierCalcFactory.Create(OutlierCalcType.ZScore);

                var bufferSize = int.Parse(ConfigurationManager.AppSettings["OutlierSpotter.BufferSize"]);
                var outlierSpotter = new OutlierSpotter(outlierAlgo, inputDataPublisher, outputPlotter, bufferSize);

                outlierSpotter.Run();

                Logger.Info("Output file created at {0}", Path.GetFullPath(ConfigurationManager.AppSettings["outputFileName"]));
                Logger.Info("Data processing completed successfully, press any key to exit.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error occured while processing outlier.");
                throw;
            }
          
            Console.ReadLine();

        }
    }
}
