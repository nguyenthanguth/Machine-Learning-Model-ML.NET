using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace LearningBuildModel
{
    public partial class MLModel
    {
        public const string RetrainFilePath = @"C:\Users\Admin\Documents\GitHub\Machine-Learning-Model-ML.NET\LearningBuildModel\iris-data.txt";

        public static void Train()
        {
            var mlContext = new MLContext();

            // var data = LoadIDataViewFromFile(mlContext, inputDataFilePath, separatorChar, hasHeader);
            // Load dữ liệu
            IDataView data = mlContext.Data.LoadFromTextFile<ModelInput>(RetrainFilePath, separatorChar: ',');

            var pipeline = mlContext.Transforms.ReplaceMissingValues(
                new[]
                {
                    new InputOutputColumnPair(@"col0", @"col0"),
                    new InputOutputColumnPair(@"col1", @"col1"),
                    new InputOutputColumnPair(@"col2", @"col2"),
                    new InputOutputColumnPair(@"col3", @"col3")
                }
                )
                .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"col0", @"col1", @"col2", @"col3" }))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"col4", inputColumnName: @"col4", addKeyValueAnnotationsAsText: false))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(new SdcaMaximumEntropyMulticlassTrainer.Options()
                {
                    L1Regularization = 0.0332272F,
                    L2Regularization = 0.03125F,
                    LabelColumnName = @"col4",
                    FeatureColumnName = @"Features"
                }))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel", inputColumnName: @"PredictedLabel"));

            var model = pipeline.Fit(data);

            using (var fs = File.Create(@"C:\Users\Admin\Documents\GitHub\Machine-Learning-Model-ML.NET\LearningBuildModel\bin\Debug\net7.0\abc.mlnet"))
            {
                mlContext.Model.Save(model, data.Schema, fs);
            }
        }
    }
}
