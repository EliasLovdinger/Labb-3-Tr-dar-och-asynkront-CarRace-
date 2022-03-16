using System;
using System.Collections.Generic;
using System.Text;

namespace Labb_3___Trådar_och_asynkront
{
    public class Car
    {
        public Car(int carId, string carName, int speed, int Fspeed)
        {
            CarId = carId;
            CarName = carName;
            Speed = speed;
            FSpeed = Fspeed;
        }

        public int CarId { get; set; }
        public string CarName { get; set; }
        public int Speed { get; set; }
        public int FSpeed { get; set; }
    }
}
