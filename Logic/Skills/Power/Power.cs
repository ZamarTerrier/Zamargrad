using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Skills.Power
{
    public class Power:Skill
    {

        public enum powerType
        {
            Урон,
            Защита,
            Характеристики,
        }

        public powerType type;
        public int value;
        public string effect;

        public Power(powerType type, string name, int value, string effect) : base(name)
        {
            this.type = type;
            this.value = value;
            this.effect = effect;

            switch (type)
            {
                case powerType.Урон:
                    cost = value * 10f;
                    break;
                case powerType.Защита:
                    cost = value * 37f;
                    break;
                case powerType.Характеристики:
                    cost = value * 20f;
                    break;
            }
        }
    }
}
