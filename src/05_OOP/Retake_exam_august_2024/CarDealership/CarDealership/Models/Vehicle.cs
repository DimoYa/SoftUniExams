using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public abstract class Vehicle : IVehicle
    {
        private string model;
        private double price;
        private List<string> buyers;

        protected Vehicle(string model, double price)
        {
            Model = model;
            Price = price;
            buyers = new List<string>();
        }

        public string Model
        {
            get => model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.ModelIsRequired);
                }
                model = value;
            }
        }

        public double Price
        {
            get => price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.PriceMustBePositive);
                }
                price = value;
            }
        }

        public IReadOnlyCollection<string> Buyers => buyers.AsReadOnly(); 

        public int SalesCount => Buyers.Count;

        public void SellVehicle(string buyerName)
        {
            buyers.Add(buyerName);
        }

        public override string ToString()
        {
            return $"{Model} - Price: {Price:F2}, Total Model Sales: {SalesCount}";
        }
    }
}
