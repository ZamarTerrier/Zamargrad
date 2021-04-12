using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Zamargrad.Modules;

namespace Zamargrad.Logic.Skills.Actions
{
    public class Action : Skill
    {
        public string effect;

        public Action(string name, string effect) : base(name)
        {
            this.effect = effect;
        }

    }
}
