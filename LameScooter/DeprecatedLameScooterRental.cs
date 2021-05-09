using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LameScooter {
    public class DeprecatedLameScooterRental : ILameScooterRental {
        public async Task<int> GetScooterCountInStation(string stationName) {
            var sr = new StreamReader("scooters.txt");
            var text = await sr.ReadToEndAsync();
            var splitText = text.Split("\r\n");
           
            var stationList = new LameScooterStationList();
            stationList.Stations = new List<Station>();
            foreach (var stationString in splitText) {
                var station = new Station();
                var splitStations = stationString.Split(" :");
                if (splitStations.Length > 1) {
                    station.Name = splitStations[0];
                    station.BikesAvailable = int.Parse(splitStations[1]);
                    stationList.Stations.Add(station);
                }
            }
            return stationList.GetAvailableBikes(stationName);
        }
    }
}