using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourierApp
{
    public class Offer
    {
        public string Code { get; }
        public double DiscountPercentage { get; }
        public double MinWeight { get; }
        public double MaxWeight { get; }
        public double MaxDistance { get; }

        public Offer(string code, double discount, double minWeight, double maxWeight, double maxDistance)
        {
            Code = code;
            DiscountPercentage = discount;
            MinWeight = minWeight;
            MaxWeight = maxWeight;
            MaxDistance = maxDistance;
        }

        public bool IsOfferApplicable(double weight, double distance )
        {
            return weight >= MinWeight &&
                   weight <= MaxWeight &&
                   distance < MaxDistance;
        }

        public static void AddOffersFromConsole(List<Offer> offers)
        {
            while (true)
            {
                Console.Write("\nDo you want to add a new offer? (Y/N): ");
                var choice = Console.ReadLine()?.ToUpper();

                if (choice != "Y")
                    break;

                string code = InputHelper.ReadString("Offer Code: ");
                double discount = InputHelper.ReadDouble("Discount Percentage: ");

                double minWeight, maxWeight;

                while (true)
                {
                    minWeight = InputHelper.ReadDouble("Enter Min Weight : ");
                    maxWeight = InputHelper.ReadDouble("Enter Max Weight : ");

                    if (minWeight < maxWeight)
                        break;

                    Console.WriteLine("Min Weight must be LESS than Max Weight. Please try again.\n");
                }


                double maxDistance = InputHelper.ReadDouble("Max Distance: ");

                offers.Add(new Offer(code, discount, minWeight, maxWeight, maxDistance));

                Console.WriteLine("Offer added successfully!");
            }
        }
    }
}
