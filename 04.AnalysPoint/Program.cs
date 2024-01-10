using _04_AnalysPoint;
using System;

namespace _04.AnalysPoint
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Hello, World!");

            //Load sample data
            var sampleData = new MLModel.ModelInput()
            {
                Col0 = 3501.964F,
                Col1 = 2743.525F,
            };

            //Load model and predict output
            MLModel.ModelOutput result = MLModel.Predict(sampleData);
            Console.WriteLine(result.Col0);
            Console.WriteLine(result.Col1);
            Console.WriteLine(result.Col2);
            Console.WriteLine(result.PredictedLabel);
            Console.WriteLine(string.Join(" ", result.Features));
            Console.WriteLine(string.Join(" ", result.Score));
        }

        public static string Analys(float X, float Y)
        {
            MLModel.ModelInput sampleData = new MLModel.ModelInput()
            {
                Col0 = X,
                Col1 = Y,
            };

            MLModel.ModelOutput predict = MLModel.Predict(sampleData);
            string show = $"{predict.PredictedLabel}\r\n{string.Join(" ", predict.Features)}\r\n{string.Join(" ", predict.Features)}";
            return show;
        }
    }
}