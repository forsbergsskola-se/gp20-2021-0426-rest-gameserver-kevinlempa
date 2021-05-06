using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LameScooter {
    class Program {
        static async Task Main(string[] args){
            ILameScooterRental rental = new OfflineLameScooterRental();
           var num = await rental.GetScooterCountInStation(args[0]);
           Console.WriteLine("Number of Scooters Available at this Station: " +num);
           
        }
    }
}