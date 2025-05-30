using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Repositories;
using CyberSecurityDS.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Models
{
    public class SystemManager : ISystemManager
    {
        private CyberAttackRepository cyberAttacks;
        private DefensiveSoftwareRepository defensiveSoftwares;

        public SystemManager()
        {
            this.cyberAttacks = new CyberAttackRepository();
            this.defensiveSoftwares = new DefensiveSoftwareRepository();
        }

        public IRepository<ICyberAttack> CyberAttacks => this.cyberAttacks;
        public IRepository<IDefensiveSoftware> DefensiveSoftwares => this.defensiveSoftwares;
    }

}
