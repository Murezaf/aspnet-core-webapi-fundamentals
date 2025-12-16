namespace Smaple01.Models
{
    /// <summary>
    /// a city without points of interest
    /// </summary>
    public class CityWithOutPointsOfInterestDto
    {
        /// <summary>
        /// The Id if the city
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The Name if the city
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The Description if the city
        /// </summary>
        public string? Description { get; set; }
    }
}
