using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningBuildModel
{
    public partial class MLModel
    {
        public class ModelInput
        {
            [LoadColumn(0)]
            [ColumnName(@"col0")]
            public float Col0 { get; set; }

            [LoadColumn(1)]
            [ColumnName(@"col1")]
            public float Col1 { get; set; }

            [LoadColumn(2)]
            [ColumnName(@"col2")]
            public float Col2 { get; set; }

            [LoadColumn(3)]
            [ColumnName(@"col3")]
            public float Col3 { get; set; }

            [LoadColumn(4)]
            [ColumnName(@"col4")]
            public string Col4 { get; set; }

        }

        public class ModelOutput
        {
            [ColumnName(@"col0")]
            public float Col0 { get; set; }

            [ColumnName(@"col1")]
            public float Col1 { get; set; }

            [ColumnName(@"col2")]
            public float Col2 { get; set; }

            [ColumnName(@"col3")]
            public float Col3 { get; set; }

            [ColumnName(@"col4")]
            public uint Col4 { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"PredictedLabel")]
            public string PredictedLabel { get; set; }

            [ColumnName(@"Score")]
            public float[] Score { get; set; }

        }

        private static string MLNetModelPath = Path.GetFullPath("model-trained.mlnet");

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }

        public static ModelOutput Predict(ModelInput input)
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);

            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            //var predEngine = CreatePredictEngine();
            return predEngine.Predict(input);
        }

    }
}
