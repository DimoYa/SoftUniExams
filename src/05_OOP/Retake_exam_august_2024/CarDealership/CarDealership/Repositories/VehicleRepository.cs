using CarDealership.Models;
using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repositories
{
    public class VehicleRepository : IRepository<IVehicle>
    {
        private List<IVehicle> models;

        public VehicleRepository()
        {
            models = new List<IVehicle>();
        }


        public IReadOnlyCollection<IVehicle> Models => models.AsReadOnly();

        public void Add(IVehicle model)
        {
            models.Add(model);
        }

        public bool Exists(string text)
        {
            return models.FirstOrDefault(m=> m.Model == text) != null;
        }

        public IVehicle Get(string text)
        {
            if (Exists(text))
            {
                return models.First(m => m.Model == text);
            }

            return null;
        }

        public bool Remove(string text)
        {
            if (Exists(text))
            {
               var model = models.First(m => m.Model == text);

                models.Remove(model);

                return true;
            }

            return false;
        }
    }
}
