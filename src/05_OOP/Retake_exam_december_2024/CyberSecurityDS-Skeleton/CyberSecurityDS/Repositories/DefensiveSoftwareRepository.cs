using CyberSecurityDS.Models;
using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Repositories
{
    public class DefensiveSoftwareRepository : IRepository<IDefensiveSoftware>
    {
        private readonly List<IDefensiveSoftware> model;

        public DefensiveSoftwareRepository()
        {
            this.model = new List<IDefensiveSoftware>();
        }

        public IReadOnlyCollection<IDefensiveSoftware> Models => model.AsReadOnly();

        public void AddNew(IDefensiveSoftware model)
        {
           this.model.Add(model);
        }

        public bool Exists(string name)
        {
            return this.model.Any(x => x.Name == name);
        }

        public IDefensiveSoftware GetByName(string name)
        {
            return this.model.First(x => x.Name == name);
        }
    }
}
