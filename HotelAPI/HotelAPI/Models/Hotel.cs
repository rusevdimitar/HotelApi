namespace HotelAPI.Models
{
    public class Hotel
    {
        public required string Code { get; set; }
        public required string HotelName { get; set; }
        public double LocalCategory { get; set; }
        public required string City { get; set; }
        public int NumberOfRooms { get; set; }
        public required List<GuestRoom> GuestRooms { get; set; }
    }
}
