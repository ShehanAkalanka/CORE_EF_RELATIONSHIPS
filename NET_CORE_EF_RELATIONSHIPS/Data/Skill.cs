using System.Text.Json.Serialization;

namespace NET_CORE_EF_RELATIONSHIPS.Data
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Damage { get; set; }

        [JsonIgnore]
        public List<Character>? Characters { get; set; }
    }
}
