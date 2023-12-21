using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using static RiskPrediction.MLModel;

namespace RiskPrediction.Controllers
{
    public class PredictionController : Controller
    {
        public IActionResult Risk(ModelInput input)
        {
            // Load the model
            MLContext mlContext = new MLContext();
            // Create predection engine related to the loaded train model
            ITransformer mlModel = mlContext.Model.Load(@"..\RiskPrediction\MLModel.zip", out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            // Try model on sample data to predict fair price
            ModelOutput result = predEngine.Predict(input);
            ViewBag.Risk = result.PredictedLabel;
            return View(input);
        }
       
    }
}
