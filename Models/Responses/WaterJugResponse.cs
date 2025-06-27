namespace WaterJug.Models.Responses
{
    public class WaterJugResponse
    {
        public bool success { get; set; }
        public List<WaterJugStep> solution { get; set; } = new();
        public string? message { get; set; }

    }
}
