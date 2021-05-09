using System;
using System.Collections.Generic;

namespace LameScooter {
    public class LameScooterStationList {
        public List<Station> Stations { get; set; }

        public int GetAvailableBikes(string stationName) {
            for (int i = 0; i < Stations.Count; i++) {
                if (stationName.Equals(Stations[i].Name,StringComparison.InvariantCultureIgnoreCase)) {
                    return Stations[i].BikesAvailable;
                }
            }
            throw new NotFoundException("This station couldn't be found ", stationName);
        }
    }
}