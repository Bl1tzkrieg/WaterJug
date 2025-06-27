namespace WaterJug.Models.Responses
{
    public class WaterJugStep
    {
        public int step { get; set; }
        public int bucketX { get; set; }
        public int bucketY { get; set; }
        public string action { get; set; } = string.Empty;
        public bool isFinalStep { get; set; } = false;

    }
}
