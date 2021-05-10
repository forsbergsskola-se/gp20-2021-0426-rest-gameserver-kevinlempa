using System;
using System.Threading.Tasks;

namespace LameScooter {
    class Program {
        static async Task Main(string[] args) {
            try {
                ILameScooterRental rental = args[1] switch {
                    "offline" => new OfflineLameScooterRental(),
                    "deprecated" => new DeprecatedLameScooterRental(),
                    "realtime" => new RealTimeLameScooterRental(),
                    _ => throw new ArgumentException($"{args[1]} is not a valid argument")
                };
                var availableBikes = await rental.GetScooterCountInStation(args[0]);
                Console.WriteLine($"Number of Scooters Available at {args[0]} : {availableBikes}");
            }
            catch (Exception e) {
                Console.WriteLine($"Invalid Argument: \r\nHere are some valid Argument examples:\r\n" +
                                  $"#1: dotnet run \"Meilahden sairaala\" offline \r\n" +
                                  $"#2: dotnet run Vantaanpuisto realtime \r\n" +
                                  $"#3: dotnet run Huopalahdentie deprecated \r\n" +
                                  $" \r\n{e.Message}");
                throw;
            }
        }
    }
}