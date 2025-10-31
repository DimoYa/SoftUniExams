using CarDealership.Core.Contracts;
using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Core
{
    public class Controller : IController
    {
        private IDealership dealership;

        public Controller()
        {
            dealership = new Dealership();
        }

        public string AddCustomer(string customerTypeName, string customerName)
        {
            ICustomer customer;

            switch (customerTypeName)
            {
                case nameof(IndividualClient):
                    customer = new IndividualClient(customerName);
                    break;
                case nameof(LegalEntityCustomer):
                    customer = new LegalEntityCustomer(customerName);
                    break;
                default:
                    return string.Format(OutputMessages.InvalidType, customerTypeName);
            }

            if (dealership.Customers.Exists(customerName))
            {
                return string.Format(OutputMessages.CustomerAlreadyAdded, customerName);
            }

            dealership.Customers.Add(customer);
            return string.Format(OutputMessages.CustomerAddedSuccessfully, customerName);
        }

        public string AddVehicle(string vehicleTypeName, string model, double price)
        {
            IVehicle vehicle;

            switch (vehicleTypeName)
            {
                case nameof(SaloonCar):
                    vehicle = new SaloonCar(model, price);
                    break;
                case nameof(SUV):
                    vehicle = new SUV(model, price);
                    break;
                case nameof(Truck):
                    vehicle = new Truck(model, price);
                    break;
                default:
                    return string.Format(OutputMessages.InvalidType, vehicleTypeName);
            }

            if (dealership.Vehicles.Exists(model))
            {
                return string.Format(OutputMessages.VehicleAlreadyAdded, model);
            }

            dealership.Vehicles.Add(vehicle);
            return string.Format(OutputMessages.VehicleAddedSuccessfully, vehicleTypeName, model, vehicle.Price.ToString("F2"));
        }

        public string CustomerReport()
        {
            var report = new StringBuilder();
            report.AppendLine("Customer Report:");

            foreach (var customer in dealership.Customers.Models.OrderBy(c => c.Name))
            {
                report.AppendLine(customer.ToString());
                report.AppendLine("-Models:");
                var vehicles = customer.Purchases.OrderBy(x => x);

                if (!vehicles.Any())
                {
                    report.AppendLine("--none");
                }
                else
                {
                    vehicles.OrderBy(x => x).ToList().ForEach(x => report.AppendLine($"--{x}"));
                }
            }

            return report.ToString().TrimEnd();
        }

        public string PurchaseVehicle(string vehicleTypeName, string customerName, double budget)
        {
            if (!dealership.Customers.Exists(customerName))
            {
                return string.Format(OutputMessages.CustomerNotFound, customerName);
            }


            if (vehicleTypeName != nameof(SaloonCar) && vehicleTypeName != nameof(SUV) && vehicleTypeName != nameof(Truck))
            {
                return string.Format(OutputMessages.VehicleTypeNotFound, vehicleTypeName);
            }

            var customer = dealership.Customers.Get(customerName);

            if (!allowedModels[customer.GetType()].Contains(vehicleTypeName))
            {
                return string.Format(OutputMessages.CustomerNotEligibleToPurchaseVehicle, customerName, vehicleTypeName);
            }

            var availableVehicles = dealership.Vehicles.Models.Where(x => x.GetType().Name == vehicleTypeName);

            if (!availableVehicles.Any(v => v.Price <= budget))
            {
                return string.Format(OutputMessages.BudgetIsNotEnough, customerName, vehicleTypeName);
            }

            var mostAffordableVehicle = availableVehicles
                .Where(v => v.Price <= budget).ToList()
                .OrderByDescending(v => v.Price)
                .FirstOrDefault();

            if (mostAffordableVehicle == null)
            {
                return string.Format(OutputMessages.BudgetIsNotEnough, customerName, vehicleTypeName);
            }

            mostAffordableVehicle.SellVehicle(customerName);
            customer.BuyVehicle(mostAffordableVehicle.Model);
            dealership.Vehicles.Remove(mostAffordableVehicle.Model);

            return string.Format(OutputMessages.VehiclePurchasedSuccessfully, customerName, mostAffordableVehicle.Model);
        }

        public string SalesReport(string vehicleTypeName)
        {
            var report = new StringBuilder();

            report.AppendLine($"{vehicleTypeName} Sales Report:");

            var filteredVehicles = dealership.Vehicles.Models
                .Where(m => m.GetType().Name == vehicleTypeName);

            foreach (var vehicle in filteredVehicles.OrderBy(x => x.Model))
            {
                report.AppendLine(vehicle.ToString());
            }

            report.AppendLine($"-Total Purchases: {filteredVehicles.Sum(x => x.SalesCount)}");

            return report.ToString().TrimEnd();
        }

        private readonly Dictionary<Type, string[]> allowedModels = new()
        {
            { typeof(IndividualClient), new[] { nameof(SaloonCar), nameof(SUV) } },
            { typeof(LegalEntityCustomer), new[] { nameof(SUV), nameof(Truck) } }
        };
    }
}
