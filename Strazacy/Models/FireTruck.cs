namespace Strazacy.Models
{
    public class FireTruck
    {
        public int IdFiretruck { get; set; }
        public string OperationNumber { get; set; }
        public bool SpecialEquipment { get; set; }
        public List<MAction> Actions { get; set; } = new List<MAction>();
    }
}
