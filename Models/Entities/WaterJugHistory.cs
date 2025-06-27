using System.ComponentModel.DataAnnotations;

namespace WaterJug.Models.Entities
{
    //
    //
    //  This is a db Entity class for the response's history table.
    //
    //
    public class WaterJugHistory
    {
        [Key]
        public int Id { get; set; }

        public int Jug1Capacity { get; set; }
        public int Jug2Capacity { get; set; }
        public int Target { get; set; }
        public bool Solved { get; set; }
        public DateTime SolvedAt { get; set; }

        public string StepsJson { get; set; } = string.Empty;
    }
}

