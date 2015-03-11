namespace BetterWeather.Data.Infrastructure
{
    public class GpsPosition
    {
        public GpsPosition(double lon, double lat)
        {
            Lon = lon;
            Lat = lat;
        }

        public double Lon { get; set; }
        public double Lat { get; set; }
    }
}