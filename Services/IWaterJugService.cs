using Microsoft.AspNetCore.Mvc;
using WaterJug.Models.Responses;

namespace WaterJug.Services
{
    public interface IWaterJugService
    {
        Task<WaterJugResponse> SolveProblem(int bucketX, int bucketY, int target);
    }
}
