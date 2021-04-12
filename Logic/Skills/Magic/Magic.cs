using System;
using System.Collections.Generic;
using System.Text;

namespace Zamargrad.Logic.Skills.Magic
{
    public class Magic: Skill
    {
        public enum magicType
        {
            Heal,
            Damage,
            PowerUp,
        }

        public magicType type;
        public int value;
        public string effect;

        public Magic(magicType type, string name, int value, string effect) : base(name)
        {
            this.type = type;
            this.value = value;
            this.effect = effect;

            switch (type)
            {
                case magicType.Damage:
                    cost = value * 1.25f;
                    break;
                case magicType.Heal:
                    cost = value * 1.05f;
                    break;
                case magicType.PowerUp:
                    cost = value * 15f;
                    break;
            }
        }

    }
}
