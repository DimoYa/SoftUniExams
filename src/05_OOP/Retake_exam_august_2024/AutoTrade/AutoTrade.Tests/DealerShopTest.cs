using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AutoTrade.Tests
{
    [TestFixture]
    public class DealerShopTests
    {
        private DealerShop sut;

        [SetUp]
        public void Setup()
        {
            this.sut = new DealerShop(10);
        }

        [Test]
        public void TestConstructorShouldReturnData()
        {
            Assert.Multiple(() =>
            {
                Assert.That(this.sut.Capacity, Is.EqualTo(10));
                Assert.That(this.sut.Vehicles, Is.Not.Null);
                Assert.That(this.sut.Vehicles.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void TestCapacityLessZeroShouldThrowEx()
        {
            Assert.That(() => new DealerShop(0), Throws
                .TypeOf<ArgumentException>()
                .With.Message.EqualTo("Capacity must be a positive value."));
        }

        [Test]
        public void TestAddFullCapacityShop()
        {
            var sut = new DealerShop(1);
            sut.AddVehicle(new Vehicle("testMake", "testModel", 2000));

            Assert.Multiple(() =>
            {
                Assert.That(() => sut.AddVehicle(new Vehicle("testMake", "testModel", 2000)), Throws
                    .TypeOf<InvalidOperationException>()
                    .With.Message.EqualTo("Inventory is full"));
                Assert.That(sut.Vehicles.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestAddVehicleSuccess()
        {
            var sut = new DealerShop(1);
            var vehicle = new Vehicle("testMake", "testModel", 2000);

            var actualMsg = sut.AddVehicle(vehicle);

            Assert.Multiple(() =>
            {
                Assert.That(actualMsg, Is.EqualTo($"Added {vehicle}"));
                Assert.That(sut.Vehicles.Count, Is.EqualTo(1));
            });
        }

        [Test]
        public void TestSellVehicleSuccess()
        {
            var sut = new DealerShop(1);
            var vehicle = new Vehicle("testMake", "testModel", 2000);
            sut.AddVehicle(vehicle);

            var actualMsg = sut.SellVehicle(vehicle);

            Assert.Multiple(() =>
            {
                Assert.That(actualMsg, Is.True);
                Assert.That(sut.Vehicles.Count, Is.EqualTo(0));
            });
        }

        [Test]
        public void TestSellNonExistentVehiclelReturnsFalse()
        {
            var sut = new DealerShop(1);
            var vehicle = new Vehicle("testMake", "testModel", 2000);

            var actualMsg = sut.SellVehicle(vehicle);

            Assert.That(actualMsg, Is.False);
        }

        [Test]
        public void TestEmptyReport()
        {
            var sut = new DealerShop(1);

            var actualMsg = sut.InventoryReport();

            Assert.That(actualMsg, Is.EqualTo("Inventory Report\r\nCapacity: 1\r\nVehicles: 0"));
        }

        [Test]
        public void TestSeveralVehiclesReport()
        {
            var sut = new DealerShop(5);

            var vehicle = new Vehicle("Lada", "Niva", 2000);
            sut.AddVehicle(vehicle);

            var vehicle2 = new Vehicle("BMW", "F20", 2020);
            sut.AddVehicle(vehicle2);

            var vehicle3 = new Vehicle("Toyota", "Yaris", 2013);
            sut.AddVehicle(vehicle3);


            var actualMsg = sut.InventoryReport();

            Assert.That(actualMsg, Is.EqualTo("Inventory Report\r\nCapacity: 5\r\nVehicles: 3\r\n2000 Lada Niva\r\n2020 BMW F20\r\n2013 Toyota Yaris"));
        }

        [Test]
        public void TestSingleVehicleInventoryReport()
        {
            var sut = new DealerShop(2);
            sut.AddVehicle(new Vehicle("Honda", "Civic", 2015));

            var report = sut.InventoryReport();

            Assert.That(report, Is.EqualTo("Inventory Report\r\nCapacity: 2\r\nVehicles: 1\r\n2015 Honda Civic"));
        }

        [Test]
        public void TestInventoryReportAfterSellingVehicle()
        {
            var sut = new DealerShop(3);
            var v1 = new Vehicle("Tesla", "Model 3", 2022);
            var v2 = new Vehicle("Volvo", "XC60", 2021);

            sut.AddVehicle(v1);
            sut.AddVehicle(v2);
            sut.SellVehicle(v1);

            var report = sut.InventoryReport();

            Assert.That(report, Is.EqualTo("Inventory Report\r\nCapacity: 3\r\nVehicles: 1\r\n2021 Volvo XC60"));
        }

        [Test]
        public void TestVehiclesCollectionShouldBeReadOnly()
        {
            var sut = new DealerShop(2);
            var vehicle = new Vehicle("Ford", "Focus", 2010);
            sut.AddVehicle(vehicle);

            Assert.That(() => ((ICollection<Vehicle>)sut.Vehicles).Add(new Vehicle("VW", "Golf", 2015)),
                Throws.TypeOf<NotSupportedException>());
        }

        [Test]
        public void TestAddingDuplicateVehiclesShouldBeAllowed()
        {
            var sut = new DealerShop(3);
            var vehicle = new Vehicle("Mazda", "3", 2018);

            sut.AddVehicle(vehicle);
            sut.AddVehicle(vehicle); // same instance

            Assert.That(sut.Vehicles.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestSellVehicleFromEmptyInventoryReturnsFalse()
        {
            var sut = new DealerShop(5);
            var vehicle = new Vehicle("Audi", "A3", 2020);

            var result = sut.SellVehicle(vehicle);

            Assert.That(result, Is.False);
        }

    }
}
