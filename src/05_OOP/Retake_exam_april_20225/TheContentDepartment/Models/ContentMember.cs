using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheContentDepartment.Utilities.Messages;

namespace TheContentDepartment.Models
{
    public class ContentMember : TeamMember
    {
        private static readonly string[] VALID_PATHS = new[] { "CSharp", "JavaScript", "Python", "Java" };

        public ContentMember(string name, string path) 
            : base(name, path)
        {
        }

        public override string Path
        {
            get { return base.Path; }
            protected set
            {
                if (!VALID_PATHS.Contains(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PathIncorrect, base.Path));
                }
                base.Path = value;
            }
        }

        public override string ToString()
        {
            return $"{Name} - {Path} path. Currently working on {InProgress.Count} tasks.";
        }
    }
}
