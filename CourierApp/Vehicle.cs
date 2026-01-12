using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp
{
    public class Vehicle
    {
        public int Id { get; }
        public double MaxWeight { get; }
        public double Speed { get; }
        public double AvailableAt { get; set; }

        public Vehicle(int id, double maxWeight, double speed)
        {
            Id = id;
            MaxWeight = maxWeight;
            Speed = speed;
            AvailableAt = 0;
        }
    }
}
