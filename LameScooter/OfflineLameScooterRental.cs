using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class OfflineLameScooterRental : ILameScooterRental  {
        public async Task<int> GetScooterCountInStation(string stationName) {
            var sr = new StreamReader("scooters.json");
            var json = await sr.ReadToEndAsync();
            var option = new JsonSerializerOptions();
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            var stations = JsonSerializer.Deserialize<LameScooterStationList>(json, option);
            
            return stations.GetAvailableBikes(stationName);
        }
    }
}