namespace WeatherTest.SampleApp.Model
{
    public class WeatherResult
    {
        public int Temperature { get; internal set; }
        public double WindSpeed { get; internal set; }
        public string Location { get; internal set; }
        public bool Success { get; internal set; }
    }
}
