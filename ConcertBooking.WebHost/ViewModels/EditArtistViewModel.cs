using TechnologyKeeda.ConcertBooking.Repositories;

namespace ConcertBooking.WebHost.ViewModels
{
    public class EditArtistViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ChooseImage { get; set; }
    }
}
