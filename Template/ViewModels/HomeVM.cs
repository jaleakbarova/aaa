using Template.Models;

namespace Template.ViewModels
{
    public class HomeVM
    {
        public List<Slider> slide { get; set; }
        public List<Blog> blog { get; set; }
        public List<Client> client { get; set; }
        public List<About> about { get; set; }
        public List<OurService> ourServices { get; set; }
    }
}
