using CarDealership.Models.Contracts;
using CarDealership.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Repositories
{
    public class CustomerRepository : IRepository<ICustomer>
    {
        private List<ICustomer> models;

        public CustomerRepository()
        {
            models = new List<ICustomer>();
        }

        public IReadOnlyCollection<ICustomer> Models => models.AsReadOnly();

        public void Add(ICustomer model)
        {
            models.Add(model);
        }

        public bool Exists(string text)
        {
            return models.FirstOrDefault(m => m.Name == text) != null;
        }

        public ICustomer Get(string text)
        {
            if (Exists(text))
            {
                return models.First(m => m.Name == text);
            }

            return null;
        }

        public bool Remove(string text)
        {
            if (Exists(text))
            {
                var model = models.First(m => m.Name == text);

                models.Remove(model);

                return true;
            }

            return false;
        }
    }
}
