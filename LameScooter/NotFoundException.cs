using System;

namespace LameScooter {
    public class NotFoundException : Exception {
        private string StationName { get; }
        public NotFoundException() {
        }

        public NotFoundException(string message)
            : base(message) {
        }

        public NotFoundException(string message, Exception inner)
            : base(message, inner) {
        }

        public NotFoundException(string message, string stationName) {
            StationName = stationName;
        }
    }
}