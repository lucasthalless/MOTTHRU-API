using Microsoft.ML;
using MOTTHRU.API.Domain.Entities;
using MOTTHRU.API.Domain.Interfaces;

namespace MOTTHRU.API.Application.UseCases
{
    public class DetectRfidAnomalyUseCase : IRfidAnomalyUseCase
    {
        private readonly MLContext _mlContext;
        private readonly ITransformer _model;

        public DetectRfidAnomalyUseCase()
        {
            _mlContext = new MLContext();
        }
        
        public RfidSignalPrediction DetectarAnomalia(List<RfidSignalData> sinais)
        {
            // Converte a lista em IDataView
            var dataView = _mlContext.Data.LoadFromEnumerable(sinais);

            // Cria o pipeline de detecção de anomalias
            var pipeline = _mlContext.Transforms.DetectIidSpike(
                outputColumnName: nameof(RfidSignalPrediction.Prediction),
                inputColumnName: nameof(RfidSignalData.Sinal),
                confidence: 95,
                pvalueHistoryLength: sinais.Count / 4
            );

            // Treina o modelo
            var model = pipeline.Fit(dataView);

            // Faz a previsão
            var transformedData = model.Transform(dataView);
            var predictions = _mlContext.Data.CreateEnumerable<RfidSignalPrediction>(transformedData, reuseRowObject: false).ToList();

            return predictions.Last();
        }

        public Task<bool> ExecuteAsync(float sinal)
        {
            var predEngine = _mlContext.Model.CreatePredictionEngine<RfidSignalData, RfidSignalPrediction>(_model);

            var input = new RfidSignalData { Sinal = sinal };
            var result = predEngine.Predict(input);

            bool anomaliaDetectada = result.Prediction[0] == 1;

            return Task.FromResult(anomaliaDetectada);
        }
    }
}