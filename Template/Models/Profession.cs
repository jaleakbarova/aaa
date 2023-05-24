namespace Template.Models
{
    public class Profession
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Client>? Clients { get; set; }
    }
}
