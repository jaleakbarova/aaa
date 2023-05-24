namespace Template.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProfessionId { get; set; }
        public Profession? profession { get; set; }
    }
}
