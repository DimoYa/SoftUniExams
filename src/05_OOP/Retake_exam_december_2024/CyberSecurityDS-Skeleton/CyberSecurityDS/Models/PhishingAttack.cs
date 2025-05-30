using CyberSecurityDS.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberSecurityDS.Models
{
    public class PhishingAttack : CyberAttack
    {
        private string targetMail;

        public PhishingAttack(string attackName, int severityLevel, string targetMail) 
            : base(attackName, severityLevel)
        {
            TargetMail = targetMail;
        }

        public string TargetMail
        {
            get => targetMail;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TargetMailRequired);
                }
                targetMail = value;
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Target Mail: {TargetMail})";
        }
    }
}
