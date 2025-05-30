using CyberSecurityDS.Models.Contracts;
using CyberSecurityDS.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Models
{
    public abstract class CyberAttack : ICyberAttack
    {
        private string attackName;
        private int severityLevel;

        protected CyberAttack(string attackName, int severityLevel)
        {
            this.AttackName = attackName;
            this.SeverityLevel = severityLevel;
        }

        public string AttackName
        {
            get => attackName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.CyberAttackNameRequired);
                }
                attackName = value;
            }
        }
        public int SeverityLevel
        {
            get => severityLevel;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.SeverityLevelNegative);
                }
                else if (value == 0)
                {
                    severityLevel = 1;
                }
                else if (value > 10)
                {
                    severityLevel = 10;
                }
                else
                {
                    severityLevel = value;
                }
            }
        }
        public bool Status { get; private set; } = false;

        public void MarkAsMitigated()
        {
            this.Status = true;
        }

        public override string ToString()
        {
            return $"Attack: {AttackName}, Severity: {SeverityLevel}";
        }
    }
}
