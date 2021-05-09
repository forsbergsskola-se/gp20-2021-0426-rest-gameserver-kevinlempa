using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LameScooter {
    class Program {
        static async Task Main(string[] args) {
            try {
                ILameScooterRental rental = new OfflineLameScooterRental();
                ILameScooterRental rental2 = new DeprecatedLameScooterRental();
                var s = await rental2.GetScooterCountInStation(args[0]);
                var num = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine("Deprecated : Number of Scooters Available at this Station: " + s);
                Console.WriteLine("Number of Scooters Available at this Station: " + num);
            }
            catch (Exception e) {
                throw new ArgumentException("Invalid argument \r\n" +e);
            }
            
        }
    }
}