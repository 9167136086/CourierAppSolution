using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp
{
    public class ScheduleVehical
    {

        public void Schedule(List<Package> packages, List<Vehicle> vehicles)
        {
            var pendingPackages = new List<Package>(packages);
            while (pendingPackages.Any())
            {
                var vehicle = vehicles.OrderBy(v => v.AvailableAt).First();
                double currentTime = vehicle.AvailableAt;
                var shipment = SelectShipment(pendingPackages, vehicle.MaxWeight);
                double maxDistance = shipment.Max(p => p.Distance);
                double travelTime = maxDistance / vehicle.Speed;

                foreach (var pkg in shipment)
                {
                    pkg.DeliveryTime = currentTime + (pkg.Distance / vehicle.Speed);
                    pendingPackages.Remove(pkg);
                }

                vehicle.AvailableAt = currentTime + (2 * travelTime);
            }
        }

        private List<Package> SelectShipment(List<Package> packages, double maxWeight)
        {
            var sorted = packages
                .OrderByDescending(p => p.Weight)
                .ToList();

            var shipment = new List<Package>();
            double totalWeight = 0;

            foreach (var pkg in sorted)
            {
                if (totalWeight + pkg.Weight <= maxWeight)
                {
                    shipment.Add(pkg);
                    totalWeight += pkg.Weight;
                }
            }

            return shipment;
        }
    }
}
