namespace ConcertBooking.WebHost.ViewModels.HomePageViewModel
{
    public class AvailabelTicketViewModel
    {

        public int ConcertId{ get; set; }
        public string ConcertName{ get; set; }
        public List<int> AvailabelSeats{ get; set; }
    
    }
}
