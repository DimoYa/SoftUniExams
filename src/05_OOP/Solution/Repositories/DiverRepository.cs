using NauticalCatchChallenge.Models;
using NauticalCatchChallenge.Models.Contracts;
using NauticalCatchChallenge.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Repositories
{
    public class DiverRepository : IRepository<IDiver>
    {
        private List<IDiver> _divers;

        public DiverRepository()
        {
            _divers = new List<IDiver>();
        }

        public IReadOnlyCollection<IDiver> Models => this._divers.AsReadOnly();

        public void AddModel(IDiver model)
        {
            this._divers.Add(model);
        }

        public IDiver GetModel(string name)
        {
            var currentDiver = this.Models.FirstOrDefault(x => x.Name == name);
            return currentDiver;
        }
    }
}
