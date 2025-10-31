using CarDealership.Models.Contracts;
using CarDealership.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models
{
    public abstract class Customer : ICustomer
    {
        private string name;
        private List<string> purchase;

        protected Customer(string name)
        {
            Name = name;
            purchase = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameIsRequired);
                }
                name = value;
            }
        }

        public IReadOnlyCollection<string> Purchases => purchase.AsReadOnly();

        public virtual void BuyVehicle(string vehicleModel)
        {
            purchase.Add(vehicleModel);
        }

        public override string ToString()
        {
            return $"{Name} - Purchases: {Purchases.Count}";
        }
    }
}
