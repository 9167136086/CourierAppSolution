using CourierApp;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;


bool restart = true;

while (restart)
{
    Console.Clear();
    Console.WriteLine("\n====================================");
Console.WriteLine("      Courier Service Setup         ");
Console.WriteLine("==================================\n");

double basicAmount = InputHelper.ReadDouble("Enter Base Delivery Amount : ");
int packageCount = (int)InputHelper.ReadDouble("Enter Number of Packages  : ");

Console.WriteLine("\n====================================");
Console.WriteLine("       Available Vehical Details      ");
Console.WriteLine(" =====================================\n");

var vehicles = new List<Vehicle>
        {
            new Vehicle(1, 200, 70),
            new Vehicle(2, 200, 70)
        };

foreach (var vehicle in vehicles)
{
    Console.WriteLine($"Vehical Number: {vehicle.Id}, Max Weight: {vehicle.MaxWeight}, Speed: {vehicle.Speed}");
}

Console.WriteLine("\n==================================");
Console.WriteLine("               Offers               ");
Console.WriteLine("==================================\n");

var offers = new List<Offer>
            {
                new Offer("OFR001", 10, 70, 200, 200),
                new Offer("OFR002", 7, 50, 150, 200),
                new Offer("OFR003", 5, 50, 250, 200)
            };

var existingCount = offers.Count();

foreach (var offer in offers)
{
    Console.WriteLine($"Code: {offer.Code}, Discount: {offer.DiscountPercentage}%, Weight Range: {offer.MinWeight}-{offer.MaxWeight}, Max Distance: {offer.MaxDistance}");
}

Offer.AddOffersFromConsole(offers);

if (existingCount < offers.Count)
{
    foreach (var offer in offers)
    {
        Console.WriteLine($"Code: {offer.Code}, Discount: {offer.DiscountPercentage}%, Weight Range: {offer.MinWeight}-{offer.MaxWeight}, Max Distance: {offer.MaxDistance}");
    }
}


Console.WriteLine("\n==================================");
Console.WriteLine("      Add Packages                 ");
Console.WriteLine("==================================\n");

var packages = new List<Package>();

Package.AddPackageFromConsole(packages, offers, basicAmount, packageCount);



ScheduleVehical scheduleVehical = new ScheduleVehical();
scheduleVehical.Schedule(packages, vehicles);

Console.WriteLine("\n================================================");
Console.WriteLine("     Package Details with Delivery Time         ");
Console.WriteLine("===============================================\n");

Console.WriteLine(
    $"{"PKG_ID",-8} {"WEIGHT",-8} {"DIST",-8} {"OFFER",-8} {"TOTAL",-8} {"DISC",-8} {"FINAL",-10} {"TIME",-8}");
Console.WriteLine(new string('-', 70));

foreach (var pkg in packages)
{
    Console.WriteLine(
        $"{pkg.Id,-8} {pkg.Weight,-8} {pkg.Distance,-8} {pkg.OfferCode,-8} {pkg.TotalCost,-8} {pkg.Discount,-8} {(pkg.TotalCost - pkg.Discount),-10} {pkg.DeliveryTime:0.00}");
}

if (packages.Count == 0)
{
    Console.WriteLine($"No Package Added..");
   
}
    Console.Write("\nDo you want to start again? (Y/N): ");
    string? choice = Console.ReadLine()?.Trim().ToUpper();
    restart = (choice == "Y");
}

Console.WriteLine("\nThank you for using the application!");
