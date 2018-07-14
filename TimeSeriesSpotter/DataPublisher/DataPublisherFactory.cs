using System.Configuration;

namespace TimeSeriesSpotter.DataPublisher
{
    /// <summary>
    /// Factory to create different data publishers
    /// </summary>
    public class DataPublisherFactory
    {
        public IDataPublisher Create(PublisherType publisherType)
        {
            if (publisherType == PublisherType.File)
            {
                var fileName = ConfigurationManager.AppSettings["inputFileName"];
                return new FileFeedPublisher(fileName);
            }

            return null;
        }
    }
}
