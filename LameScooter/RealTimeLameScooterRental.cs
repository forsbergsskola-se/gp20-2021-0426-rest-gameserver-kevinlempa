using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LameScooter {
    public class RealTimeLameScooterRental : ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            var httpClient = new HttpClient();
            var responseText = "";
            try
            {
                responseText = httpClient.GetStringAsync("https://raw.githubusercontent.com/marczaku/GP20-2021-0426-Rest-Gameserver/main/assignments/scooters.json").Result;
            }
            catch (Exception e)
            {
                throw new Exception("Get request failed. @ \r\n " +this +e);
            }
            
            var option = new JsonSerializerOptions();
            option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            var stations = JsonSerializer.Deserialize<LameScooterStationList>(responseText, option);
            
            return stations.GetAvailableBikes(stationName);
        }
    }
}