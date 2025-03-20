namespace ConcertBooking.WebHost.ViewModels
{
    public class CreateConcertViewModel
    {

        public string Name {  get; set; }   
        public string Description {  get; set; }    
        public IFormFile Imageurl { get; set; }
        public DateTime DateTime { get; set; }  
        public int VenueId {  get; set; }   
        public int ArtistId {  get; set; }  
    }
}
