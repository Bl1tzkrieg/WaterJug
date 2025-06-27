namespace WaterJug.Models
{
    public class State
    {
        public int x { get; set; }
        public int y { get; set; }
        public State? prev { get; set; }
        public string action { get; set; }

        public State(int x, int y, State? prev, string action)
        {
            this.x = x;
            this.y = y;
            this.prev = prev;
            this.action = action;
        }
    }
}
