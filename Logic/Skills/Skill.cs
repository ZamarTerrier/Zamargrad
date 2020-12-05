using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Skills
{

    abstract public class Skill
    {
        public string name;
        public float cost;

        public Skill(string name)
        {
            this.name = name;
        }

    }
}
