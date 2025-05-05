using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Models.Contracts;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public abstract class Resource : IResource
    {
        private string name;

        protected Resource(string name, string creator, int priority)
        {
            Name = name;
            Creator = creator;
            Priority = priority;
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhiteSpace);
                }
                name = value;
            }
        }

        public string Creator { get; private set; }

        public int Priority { get; private set; }

        public bool IsTested { get; private set; } = false;

        public bool IsApproved { get; private set; } = false;

        public void Approve()
        {
            IsApproved = true;
        }

        public void Test()
        {
            IsTested = !IsTested;
        }

        public override string ToString()
        {
            return $"{Name} ({this.GetType().Name}), Created By: {Creator}";
        }
    }
}
