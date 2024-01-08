using _01_Multi_class_Classification;

namespace _01.Multi_class_Classification
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Load sample data
            var sampleData = new MLModel.ModelInput()
            {
                Col0 = 6.8F,
                Col1 = 3.0F,
                Col2 = 5.5F,
                Col3 = 2.1F,
            };

            //Load model and predict output
            var result = MLModel.Predict(sampleData);

            Console.WriteLine(result.PredictedLabel);
        }
    }
}