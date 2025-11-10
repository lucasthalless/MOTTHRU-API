using Microsoft.ML.Data;

namespace MOTTHRU.API.Domain.Entities;


public class RfidSignalPrediction
{
    [VectorType]
    public double[] Prediction { get; set; } = default!;
}