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
            for (int i = 0; i < stations.Stations.Length; i++) {
                if (stationName == stations.Stations[i].Name) {
                    return stations.Stations[i].BikesAvailable;
                }
            }
            return -1;
        }
    }
}