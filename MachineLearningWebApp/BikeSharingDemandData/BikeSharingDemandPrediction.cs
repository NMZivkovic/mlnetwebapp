using Microsoft.ML.Runtime.Api;

namespace MachineLearningWebApp.BikeSharingDemandData
{
    public class BikeSharingDemandPrediction
    {
        [ColumnName("Score")]
        public float PredictedCount;
    }
}
