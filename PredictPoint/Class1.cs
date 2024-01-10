using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Microsoft.ML.Data;
using static PredictPoint.MLModel;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;

namespace PredictPoint
{
    public class Class1
    {
        [CommandMethod("Hello")]
        public static void Hello()
        {
            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("hello world net framework 4.8");
        }

        [CommandMethod("Predict")]
        public static void Predict()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            while (true)
            {
                PromptPointResult ppr = ed.GetPoint("Pick");
                if (ppr.Status != PromptStatus.OK)
                {
                    return;
                }
                Point3d point = ppr.Value;

                ModelInput modelInput = new ModelInput();
                modelInput.Col0 = Convert.ToSingle(point.X);
                modelInput.Col1 = Convert.ToSingle(point.Y);

                ModelOutput modelOutput = MLModel.Predict(modelInput);
                for (int i = 0; i < modelOutput.Score.Length; i++)
                {
                    modelOutput.Score[i] = Convert.ToInt32(modelOutput.Score[i] * 100);
                }

                MessageBox.Show($"Nhóm: {modelOutput.PredictedLabel}\r\n" +
                    $"Score: {string.Join("%\t", modelOutput.Score)}\r\n" +
                    $"Distance: {string.Join("mm\t", modelOutput.Features)}");

            }
        }
    }
}
