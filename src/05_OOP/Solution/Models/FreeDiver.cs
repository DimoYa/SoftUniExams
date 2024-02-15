using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticalCatchChallenge.Models
{
    public class FreeDiver : Diver
    {
        private const int OXYGEN_LEVEL = 120;

        public FreeDiver(string name) 
            : base(name, OXYGEN_LEVEL)
        {
        }

        public override void Miss(int timeToCatch)
        {
            this.OxygenLevel -= (int)Math.Round(0.6 * timeToCatch, MidpointRounding.AwayFromZero);
        }

        public override void RenewOxy()
        {
            this.OxygenLevel = OXYGEN_LEVEL;
        }
    }
}
