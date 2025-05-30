using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Models
{
    public abstract class DefensiveSoftware : IDefensiveSoftware
    {
        private string name;
        private int effectiveness;
        private List<string> assignedAttacks;

        protected DefensiveSoftware(string name, int effectiveness)
        {
            this.Name = name;
            this.Effectiveness = effectiveness;
            this.assignedAttacks = new List<string>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.SoftwareNameRequired);
                }
                name = value;
            }
        }

        public int Effectiveness
        {
            get => effectiveness;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.EffectivenessNegative);
                }
                else if (value == 0)
                {
                    effectiveness = 1;
                }
                else if (value > 10)
                {
                    effectiveness = 10;
                }
                else
                {
                    effectiveness = value;
                }
            }
        }

        public IReadOnlyCollection<string> AssignedAttacks => assignedAttacks.AsReadOnly();

        public virtual void AssignAttack(string attackName)
        {
            this.assignedAttacks.Add(attackName);
        }

        public override string ToString()
        {
            var attackers = this.assignedAttacks.Any() ? string.Join(", ", this.assignedAttacks) : "[None]";
            return $"Defensive Software: {Name}, Effectiveness: {Effectiveness}, Assigned Attacks: {attackers}";
        }
    }
}
