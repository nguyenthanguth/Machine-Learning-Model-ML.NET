using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Immutable;
using System;

namespace LearningBuildModel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BuildModel();
            PredictModel();

            Console.ReadLine();
        }

        private static void BuildModel()
        {
            // Initialize MLContext
            MLContext mlContext = new MLContext();

            // Load dữ liệu
            IDataView dataView = mlContext.Data.LoadFromTextFile<IrisInput>("./iris-data.txt", separatorChar: ',');

            // Tách dữ liệu thành 2 phần: 80% cho train và 20% cho test
            TrainTestData split = mlContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            /*
             * Chọn thuật toán, giải thuật => build pipeline
             */
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

            // Train model
            ITransformer model = pipeline.Fit(split.TrainSet); // split.TrainSet || dataView

            // Đánh giá model
            var classificationMetrics = mlContext.MulticlassClassification.Evaluate(model.Transform(split.TestSet), "col4");
            ShowAllProperties(classificationMetrics);

            // Lưu model
            mlContext.Model.Save(model, split.TrainSet.Schema, "./model-trained.zip");
            using (var fs = File.Create("./model-trained.mlnet"))
            {
                mlContext.Model.Save(model, dataView.Schema, fs);
            }
        }

        private static void PredictModel()
        {
            IrisInput sampleData = new IrisInput
            {
                Col0 = 6.8f,
                Col1 = 3.0f,
                Col2 = 5.5f,
                Col3 = 2.1f
            };

            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load("./model-trained.mlnet", out var _);

            var predictionEngine = mlContext.Model.CreatePredictionEngine<IrisInput, IrisOutput>(mlModel);
            IrisOutput predict = predictionEngine.Predict(sampleData);
            ShowAllProperties(predict);
        }

        public class IrisInput
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

        public class IrisOutput
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
    
        private static void ShowAllProperties(object obj)
        {
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            // In ra từng thuộc tính và giá trị của nó
            foreach (PropertyInfo property in propertyInfos)
            {
                var value = property.GetValue(obj);
                Console.WriteLine($"{property.Name}: \t {value}");

                // Kiểm tra xem giá trị là một ImmutableArray<double>
                if (value is ImmutableArray<double> immutableArrayValue)
                {
                    Console.Write($"{property.Name}: \t [");
                    for (int i = 0; i < immutableArrayValue.Length; i++)
                    {
                        Console.Write($"{immutableArrayValue[i]}{(i < immutableArrayValue.Length - 1 ? ", " : "")}");
                    }
                    Console.WriteLine("]");
                }
                else if (value is Single[] stringArrayValue)
                {
                    Console.Write($"{property.Name}: [");
                    for (int i = 0; i < stringArrayValue.Length; i++)
                    {
                        Console.Write($"\"{stringArrayValue[i]}\"{(i < stringArrayValue.Length - 1 ? ", " : "")}");
                    }
                    Console.WriteLine("]");
                }
            }
            Console.WriteLine("===============================================");
        }
    }
}