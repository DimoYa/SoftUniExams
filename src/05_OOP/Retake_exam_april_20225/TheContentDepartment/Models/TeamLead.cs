using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class TeamLead : TeamMember
    {
        private const string VALID_PATH = "Master";

        public TeamLead(string name, string path) 
            : base(name, path)
        {
        }

        public override string Path
        {
            get { return base.Path; }
            protected set
            {
                if (value != VALID_PATH)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, base.Path));
                }
                base.Path = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} ({this.GetType().Name}) – Currently working on {InProgress.Count} tasks.";
        }
    }
}
