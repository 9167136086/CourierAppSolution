using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp
{
    public class Package
    {
        public string Id { get; }
        public double Weight { get; }
        public double Distance { get; }
        public string OfferCode { get; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double DeliveryTime { get; set; }

        public Package(string id, double weight, double distance, string offerCode, double totalCost, double discount)
        {
            Id = id;
            Weight = weight;
            Distance = distance;
            OfferCode = offerCode;
            TotalCost = totalCost;
            Discount= discount;
        }


        public static void AddPackageFromConsole(List<Package> packages, List<Offer> offers,double baseCost,int packageCount)
        {
            int i = 0;
            while (true && i!=packageCount)
            {
                Console.Write("\nAdd a new Package? (Y/N): ");
                var choice = Console.ReadLine()?.ToUpper();

                if (choice != "Y")
                    break;

                string id = "PKG" + packages.Count + 1;
                double weight = InputHelper.ReadDouble("Weight: ");
                double distance = InputHelper.ReadDouble("Distance: ");

                Console.WriteLine("        Available Offer Codes: " +
                                                                (offers.Any()
                                                                    ? string.Join(", ", offers.Select(o => o.Code))
                                                                    : "None")
                                                                );
                string offerCode = InputHelper.ReadString("Offer Code: ");

                double totalCost = baseCost + (weight * 10) + (distance * 5);

                double offerDiscount = 0;

                if (offers.Any(o => o.Code == offerCode))
                {

                    var offer = offers.FirstOrDefault(o => o.Code == offerCode);

                    if (offer != null && offer.IsOfferApplicable(weight, distance))
                    {
                        offerDiscount = totalCost * offer.DiscountPercentage / 100;
                    }                  

                }

                packages.Add(new Package(id, weight, distance, offerCode, totalCost, offerDiscount));

                Console.WriteLine("Package added successfully!");
                i++;
            }
        }
    }
}
