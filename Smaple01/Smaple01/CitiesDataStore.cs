using Smaple01.Models;

namespace Smaple01
{
    public class CitiesDataStore
    {
        public List<CityDto> cities { get; set; }
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            cities = new List<CityDto>()
            {
                new CityDto(){Id = 1, Name = "NewYork", Description = "best place to live in"
                , PointsOfInterest = new List<PointOfInterestDto>() {new PointOfInterestDto(){Id = 1, Name = "FreedomStatus", Description ="gourgeos"},
                new PointOfInterestDto(){ Id = 2, Name = "Parks", Description = "Clean"} } },


                new CityDto(){Id = 2, Name = "London", Description = "always rainy"
                , PointsOfInterest = new List<PointOfInterestDto>() {new PointOfInterestDto(){Id = 3, Name = "Clock", Description ="big"},
                new PointOfInterestDto(){ Id = 4, Name = "PhoneStation", Description = "red"} } },


                new CityDto(){Id = 3, Name = "Paris", Description = "most romance city"
                , PointsOfInterest = new List < PointOfInterestDto >(){ new PointOfInterestDto() { Id = 5, Name = "eiffel", Description = "Wonderful"} } }
            };
        }
    }
}
