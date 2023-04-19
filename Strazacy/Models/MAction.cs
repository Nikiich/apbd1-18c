using System.Text.Json.Serialization;

namespace Strazacy.Models
{
    public class MAction
    {
        public int IdAction { get; set; }
        public DateTime StartTime { get; set; }
        [JsonIgnore]
        public DateTime? EndTime { get; set; }
        public bool NeedSpecialEquipment { get; set; }
        public List<FireTruck> Firetrucks { get; set; } = new List<FireTruck>();
        public List<Firefighter> Firefighters { get; set; } = new List<Firefighter>();
    }
}
