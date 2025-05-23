using Microsoft.ML;
using Microsoft.ML.Data;
using OdontoGuardAPI.Models;

namespace OdontoGuardAPI.Services
{
    public class SentimentAnalysisService
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public SentimentAnalysisService()
        {
            _mlContext = new MLContext();

            // Definir o pipeline de transformação e treinamento
            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(SentimentModel.Text))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(SentimentModel.Text)))
                .Append(_mlContext.Regression.Trainers.Sdca(labelColumnName: "Label", maximumNumberOfIterations: 100));

            // Exemplo de dados de treinamento
            var trainingData = _mlContext.Data.LoadFromEnumerable(new List<SentimentModel>
            {
                new SentimentModel { Text = "Estou muito feliz com o serviço!" },
                new SentimentModel { Text = "Esse produto é péssimo!" },
            });

            // Treinamento do modelo
            _model = pipeline.Fit(trainingData);
        }

        public SentimentPrediction PredictSentiment(SentimentModel sentimentModel)
        {
            var predictionFunction = _mlContext.Model.CreatePredictionEngine<SentimentModel, SentimentPrediction>(_model);
            var prediction = predictionFunction.Predict(sentimentModel);
            return prediction;
        }
    }
}
