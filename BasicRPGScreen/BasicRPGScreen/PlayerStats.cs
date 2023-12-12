using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BasicRPGScreen
{
    public class PlayerStats
    {
        //[JsonPropertyName("Name")]
        public string Name { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }

        //[JsonPropertyName("Max HP")]
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }

        public int Strength { get; set; }
        public int Agility { get; set; }
        public int Magic { get; set; }
        //[JsonPropertyName("Physical Defense")]
        public int PhysicalDefense { get; set; }
        //[JsonPropertyName("Magical Defense")]
        public int MagicalDefense { get; set; }
        public int Speed { get; set; }

        //[JsonPropertyName("Heal Count")]
        public int HealCount { get; set; }
    }
}
