using Microsoft.ML;
using Microsoft.ML.Data;
using System.Text;
using static Microsoft.ML.DataOperationsCatalog;

namespace LearningBuildModel
{
    internal class Program
    {
        public class IrisData
        {
            [LoadColumn(0)]
            public float SepalLength;

            [LoadColumn(1)]
            public float SepalWidth;

            [LoadColumn(2)]
            public float PetalLength;

            [LoadColumn(3)]
            public float PetalWidth;

            [LoadColumn(4)]
            public string Label;
        }

        public class IrisPrediction
        {
            [ColumnName("PredictedLabel")]
            public uint PredictedClustedId;

            [ColumnName("Score")]
            public float[] Distances;
        }

        public class IrisTest
        {
            internal static readonly IrisData Setosa = new IrisData
            {
                SepalLength = 5.1f,
                SepalWidth = 3.5f,
                PetalLength = 1.4f,
                PetalWidth = 0.2f
            };
        }

        static void Main(string[] args)
        {
            // Initialize MLContext
            var context = new MLContext();

            // Read data from file
            var data = context.Data.LoadFromTextFile<IrisData>("iris-data.txt", separatorChar: ',');

            // Split data into training and testing sets
            var split = context.Data.TrainTestSplit(data);

            // Build pipeline
            var pipeline = context.Transforms.Concatenate("Features",
                "SepalLength",
                "SepalWidth",
                "PetalLength",
                "PetalWidth")
                .Append(context.Clustering.Trainers.KMeans("Features", numberOfClusters: 3));

            // Train the model
            var model = pipeline.Fit(split.TrainSet);

            //5.giả sử mô hình ngon rồi thì lưu mô hình lại
            context.Model.Save(model, split.TrainSet.Schema, "model-trained.zip");

            // dự đoán
            var predictor = context.Model.CreatePredictionEngine<IrisData, IrisPrediction>(model);

            var prediction = predictor.Predict(IrisTest.Setosa);
            Console.WriteLine(prediction.PredictedClustedId);
            Console.WriteLine(string.Join(" ", prediction.Distances));
            Console.ReadLine();
        }
    }
}