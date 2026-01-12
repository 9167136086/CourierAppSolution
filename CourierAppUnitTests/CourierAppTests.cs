using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using CourierApp;

namespace CourierAppUnitTests
{
    [TestFixture]
    public class VehicleTests
    {
        [Test]
        public void Constructor_SetsProperties()
        {
            var vehicle = new Vehicle(3, 250.0, 80.0);

            Assert.AreEqual(3, vehicle.Id);
            Assert.AreEqual(250.0, vehicle.MaxWeight);
            Assert.AreEqual(80.0, vehicle.Speed);
            Assert.AreEqual(0.0, vehicle.AvailableAt);
        }
    }

    [TestFixture]
    public class OfferTests
    {
        [Test]
        public void IsOfferApplicable_WithinRange_ReturnsTrue()
        {
            var offer = new Offer("O1", 10, 50, 150, 200);

            Assert.IsTrue(offer.IsOfferApplicable(75, 100));
        }

        [Test]
        public void IsOfferApplicable_DistanceAtBoundary_ReturnsFalse()
        {
            var offer = new Offer("O1", 10, 50, 150, 200);

            // distance == MaxDistance should be not applicable because implementation uses '< MaxDistance'
            Assert.IsFalse(offer.IsOfferApplicable(75, 200));
        }

        [Test]
        public void AddOffersFromConsole_AddsOffer_WhenUserProvidesValidInput()
        {
            var offers = new List<Offer>();
            var input = new StringReader(string.Join(Environment.NewLine, new[]
            {
                "Y",        // Do you want to add a new offer?
                "NEW1",     // Offer Code
                "12",       // Discount Percentage
                "10",       // Min Weight
                "20",       // Max Weight
                "100",      // Max Distance
                "N"         // Stop adding
            }));

            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                Console.SetIn(input);
                using var sw = new StringWriter();
                Console.SetOut(sw);

                Offer.AddOffersFromConsole(offers);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }

            Assert.AreEqual(1, offers.Count);
            var added = offers[0];
            Assert.AreEqual("NEW1", added.Code);
            Assert.AreEqual(12.0, added.DiscountPercentage);
            Assert.AreEqual(10.0, added.MinWeight);
            Assert.AreEqual(20.0, added.MaxWeight);
            Assert.AreEqual(100.0, added.MaxDistance);
        }
    }

    [TestFixture]
    public class InputHelperTests
    {
        [Test]
        public void ReadString_ReturnsTrimmedNonEmpty()
        {
            var input = new StringReader("   hello world  " + Environment.NewLine);
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                Console.SetIn(input);
                using var sw = new StringWriter();
                Console.SetOut(sw);

                var result = InputHelper.ReadString("msg: ");
                Assert.AreEqual("hello world", result);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Test]
        public void ReadString_RetriesWhenEmptyThenReturns()
        {
            var input = new StringReader(Environment.NewLine + "abc" + Environment.NewLine);
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                Console.SetIn(input);
                using var sw = new StringWriter();
                Console.SetOut(sw);

                var result = InputHelper.ReadString("msg: ");
                Assert.AreEqual("abc", result);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }

        [Test]
        public void ReadDouble_RetriesOnInvalidThenReturnsDouble()
        {
            var input = new StringReader("notANumber" + Environment.NewLine + "12.5" + Environment.NewLine);
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                Console.SetIn(input);
                using var sw = new StringWriter();
                Console.SetOut(sw);

                var result = InputHelper.ReadDouble("enter: ");
                Assert.AreEqual(12.5, result);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }
        }
    }

    [TestFixture]
    public class PackageTests
    {
        [Test]
        public void AddPackageFromConsole_AddsPackageAndAppliesOfferWhenApplicable()
        {
            var packages = new List<Package>();
            var offers = new List<Offer>
            {
                new Offer("OFR001", 10, 70, 200, 200)
            };

            double baseCost = 100.0;
            int packageCount = 1;

            // Provide inputs: Add? Y, weight, distance, offerCode
            var inputLines = new[]
            {
                "Y",       // Add a new Package? (Y/N)
                "80",      // Weight
                "100",     // Distance
                "OFR001"   // Offer Code
            };

            var input = new StringReader(string.Join(Environment.NewLine, inputLines) + Environment.NewLine);
            var originalIn = Console.In;
            var originalOut = Console.Out;

            try
            {
                Console.SetIn(input);
                using var sw = new StringWriter();
                Console.SetOut(sw);

                Package.AddPackageFromConsole(packages, offers, baseCost, packageCount);
            }
            finally
            {
                Console.SetIn(originalIn);
                Console.SetOut(originalOut);
            }

            Assert.AreEqual(1, packages.Count);
            var pkg = packages[0];

            // The implementation builds id with "PKG" + packages.Count + 1 at creation time; when packages.Count==0 => "PKG0" + 1 => "PKG01"
            Assert.AreEqual("PKG01", pkg.Id);
            Assert.AreEqual(80.0, pkg.Weight);
            Assert.AreEqual(100.0, pkg.Distance);
            Assert.AreEqual("OFR001", pkg.OfferCode);

            var expectedTotal = baseCost + (80.0 * 10) + (100.0 * 5); // base + weight*10 + distance*5
            var expectedDiscount = expectedTotal * 0.10; // 10%
            Assert.AreEqual(expectedTotal, pkg.TotalCost);
            Assert.AreEqual(expectedDiscount, pkg.Discount);
        }
    }

    [TestFixture]
    public class ScheduleVehicalTests
    {
        [Test]
        public void Schedule_SingleVehicle_AllPackagesDeliveredWithCorrectTimes()
        {
            var packages = new List<Package>
            {
                new Package("A", 100, 100, "", 0, 0),
                new Package("B", 80, 50, "", 0, 0)
            };

            var vehicles = new List<Vehicle>
            {
                new Vehicle(1, 200, 50) // speed 50
            };

            var scheduler = new ScheduleVehical();
            scheduler.Schedule(packages, vehicles);

            // For both packages shipped together:
            // travelTime = maxDistance / speed = 100 / 50 = 2
            // DeliveryTime for A = 100 / 50 = 2
            // DeliveryTime for B = 50 / 50 = 1
            Assert.That(packages.Single(p => p.Id == "A").DeliveryTime, Is.EqualTo(2.0).Within(1e-6));
            Assert.That(packages.Single(p => p.Id == "B").DeliveryTime, Is.EqualTo(1.0).Within(1e-6));

            // Vehicle AvailableAt = 0 + (2 * travelTime) = 4
            Assert.That(vehicles[0].AvailableAt, Is.EqualTo(4.0).Within(1e-6));
        }

        [Test]
        public void Schedule_TwoVehicles_DistributesShipmentsCorrectly()
        {
            var pkg1 = new Package("P1", 150, 100, "", 0, 0);
            var pkg2 = new Package("P2", 100, 50, "", 0, 0);
            var pkg3 = new Package("P3", 80, 30, "", 0, 0);

            var packages = new List<Package> { pkg1, pkg2, pkg3 };

            var vehicles = new List<Vehicle>
            {
                new Vehicle(1, 200, 50),
                new Vehicle(2, 200, 50)
            };

            var scheduler = new ScheduleVehical();
            scheduler.Schedule(packages, vehicles);

            // Expectation:
            // First shipment (vehicle with AvailableAt 0, tied -> first in list) selects pkg1 only (150) because 150 + next 100 > 200 and 150 + 80 > 200.
            // DeliveryTime for pkg1 = 100 / 50 = 2
            Assert.That(pkg1.DeliveryTime, Is.EqualTo(2.0).Within(1e-6));
            Assert.That(vehicles[0].AvailableAt, Is.EqualTo(4.0).Within(1e-6)); // 2 * travelTime (2)

            // Remaining packages (100 and 80) should be shipped by the other vehicle (AvailableAt 0)
            // For vehicle 2, maxDistance = 50 -> travelTime = 50/50 = 1
            // Delivery times: pkg2 = 50/50 = 1, pkg3 = 30/50 = 0.6
            Assert.That(pkg2.DeliveryTime, Is.EqualTo(1.0).Within(1e-6));
            Assert.That(pkg3.DeliveryTime, Is.EqualTo(0.6).Within(1e-6));
            Assert.That(vehicles[1].AvailableAt, Is.EqualTo(2.0).Within(1e-6)); // 2 * travelTime (1)
        }
    }
}