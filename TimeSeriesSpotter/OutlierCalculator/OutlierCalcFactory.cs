using System.Configuration;
using TimeSeriesSpotter.Helper;

namespace TimeSeriesSpotter.OutlierCalculator
{
    /// <summary>
    /// Factory to create Outlier calculation algorithm instances
    /// </summary>
    public class OutlierCalcFactory
    {
        public IOutlierCalculator Create(OutlierCalcType calcType)
        {
            if (calcType == OutlierCalcType.ZScore)
            {
                var trainingDataSize = int.Parse(ConfigurationManager.AppSettings["ZScoreDeviation.TraingSetSize"]);
                var outlierThreshHold = double.Parse(ConfigurationManager.AppSettings["ZScoreDeviation.OutlierThreshHold"]);
                var statsHelper = new StatsHelper();
                return new ZScoreDeviation(trainingDataSize, outlierThreshHold, statsHelper);
            }

            return null;
        }
    }
}
