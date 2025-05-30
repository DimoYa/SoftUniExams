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
    public class CyberAttackRepository : IRepository<ICyberAttack>
    {
        private readonly List<ICyberAttack> model;
        public CyberAttackRepository()
        {
            this.model = new List<ICyberAttack>();
        }
        public IReadOnlyCollection<ICyberAttack> Models => model.AsReadOnly();

        public void AddNew(ICyberAttack model)
        {
            this.model.Add(model);
        }

        public bool Exists(string name)
        {
            return this.model.Any(m => m.AttackName == name);
        }

        public ICyberAttack GetByName(string name)
        {
            return this.model.FirstOrDefault(a => a.AttackName == name);
        }
    }
}
