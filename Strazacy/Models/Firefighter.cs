using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Strazacy.Models
{
    public class Firefighter
    {
        public int IdFirefighter{ get; set; }
        [MaxLength(200)]
        public string FirstName { get; set; }
        [MaxLength(200)]
        public string LastName { get; set; }
        [JsonIgnore]
        public List<MAction> Actions { get; set; }
    }
}
