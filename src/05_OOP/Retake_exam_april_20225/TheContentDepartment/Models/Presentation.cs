﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheContentDepartment.Models
{
    public class Presentation : Resource
    {
        private const int PRIORITY_LEVEL = 3;

        public Presentation(string name, string creator)
            : base(name, creator, PRIORITY_LEVEL)
        {
        }
    }
}
