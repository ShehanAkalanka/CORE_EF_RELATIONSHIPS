namespace NET_CORE_EF_RELATIONSHIPS.Data
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public List<Character>? Characters { get; set; }

    }
}
